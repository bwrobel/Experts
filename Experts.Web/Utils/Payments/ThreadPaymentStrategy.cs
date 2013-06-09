using System.Web.Mvc;
using Experts.Core.Entities;
using Experts.Core.Entities.Events;
using Experts.Web.Helpers;

namespace Experts.Web.Utils.Payments
{
    public class ThreadPaymentStrategy : PaymentStrategy<Thread>
    {
        public override ActionResult PostPaymentAction
        {
            get { return MVC.Thread.ThreadDetails(Payment.RelatedId); }
        }

        public override decimal PaymentAmount
        {
            get { return RelatedService.Value; }
        }

        public static string StrategyName { get { return typeof(ThreadPaymentStrategy).FullName; } }

        public static int StrategyId { get { return 1; } }

        protected override Thread GetRelatedService(int relatedId)
        {
            return Repository.Thread.Get(relatedId);
        }

        public override void UpdateRelatedService()
        {
            Repository.Thread.Update(RelatedEntity);
        }

        public override void BeforePayment()
        {
            var thread = Repository.Thread.Get(Payment.RelatedId);
            var user = AuthenticationHelper.CurrentUser;

            if(AuthenticationHelper.IsAuthenticated && thread.Author == null)
            {
                thread.Author = user;
                foreach(var post in thread.Posts)
                {
                    post.Author = user;
                    foreach (var attachment in post.Attachments)
                        attachment.Author = user;
                }
            }
        }

        public override void AfterPayment()
        {            
            base.AfterPayment();

            #region Aktualizacja wątku

            //Zasilenie konta nową gotówką
            if (Payment.Amount > decimal.Zero) RelatedService.AddTransfer(Transfer.Cash(Payment.Amount, string.Format(Resources.Payment.TransferCashThreadPayment, RelatedService.Name), Payment), Payment.User);
            //Pobranie opłaty za usługę
            RelatedService.AddTransfer(Transfer.Pending(-PaymentAmount, string.Format(Resources.Payment.TransferPendingThreadPayment, RelatedService.Name)), Payment.User);
            //Oznaczenie że usługa została opłacona
            RelatedService.IsPaid = true;

            UpdateRelatedService();

            Repository.User.Update(Payment.User);

            #endregion

            Email.Send<ThreadPaymentConfirmationEmail>(RelatedEntity);
            Log.Event<ThreadPayedEvent>(RelatedEntity);

            var expertsToNotify = Repository.Expert.FindClosestMatches(RelatedEntity.Category.Id, RelatedEntity.CategoryAttributes, 30);
            int expertsToNotifyCount = 0;
            foreach (var expert in expertsToNotify)
            {
                if(RelatedEntity.IsInner && !expert.IsInner)
                {
                    
                }
                else
                {
                    Email.Send<NewInterestingThreadEmail>(RelatedEntity, expert);
                    expertsToNotifyCount += 1;   
                }
            }

            Log.Event<ExpertThreadNotifiedEvent>(RelatedEntity, expertsToNotifyCount.ToString());
        }
    }
}
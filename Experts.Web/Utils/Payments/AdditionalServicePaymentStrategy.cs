using System;
using System.Linq;
using System.Web.Mvc;
using Experts.Core.Entities;
using Experts.Core.Entities.Events;
using Experts.Web.Helpers;

namespace Experts.Web.Utils.Payments
{
    public class AdditionalServicePaymentStrategy : PaymentStrategy<AdditionalService>
    {
        public override ActionResult PostPaymentAction
        {
            get
            {
                var thread = GetRelatedService(Payment.RelatedId).Thread;
                return MVC.Thread.ThreadDetails(thread.Id);
            }
        }

        public override decimal PaymentAmount
        {
            get { return RelatedService.Value; }
        }

        public static string StrategyName { get { return typeof(AdditionalServicePaymentStrategy).FullName; } }

        public static int StrategyId { get { return 3; } }

        protected override AdditionalService GetRelatedService(int relatedId)
        {
            return Repository.Thread.GetAdditionalService(relatedId);
        }

        public override void UpdateRelatedService()
        {
            Repository.Thread.UpdateThreadByAdditionalService(RelatedEntity);
        }

        public override void AfterPayment()
        {
            base.AfterPayment();

            var additionalService = GetRelatedService(Payment.RelatedId);
            var thread = additionalService.Thread;

            //Zasilenie konta nową gotówką
            if (Payment.Amount > decimal.Zero)
                RelatedService.AddTransfer(
                    Transfer.Cash(Payment.Amount,
                                  string.Format(Resources.Payment.TransferCashAdditionalServicePayment, RelatedService.Name),
                                  Payment), Payment.User);
            //Pobranie opłaty za usługę
            RelatedService.AddTransfer(
                Transfer.Pending(-PaymentAmount,
                                 string.Format(Resources.Payment.TransferPendingAdditionalServicePayment,
                                               RelatedService.Name)), Payment.User);
            //Oznaczenie że usługa została opłacona
            RelatedService.IsPaid = true;
            additionalService.LastModificationDate = DateTime.Now;
            additionalService.IsAccepted = true;
            thread.Value += additionalService.Value;

            UpdateRelatedService();

            Repository.User.Update(Payment.User);

            Repository.Thread.AddPost(thread, SystemPostFactory.BuildAdditionalServicePost(Repository.Consultant.All().First(), additionalService.Title));

            EventLog.Event<AdditionalServicePayedEvent>(thread, additionalService.Title);
            Email.Send<AdditionalServiceAnswerEmail>(additionalService);
        }
    }
}
using System.Linq;
using System.Web.Mvc;
using Experts.Core.Entities;
using Experts.Core.Entities.Events;
using Experts.Web.Helpers;

namespace Experts.Web.Utils.Payments
{
    public class ThreadSurchargeStrategy : ThreadPaymentStrategy
    {
        public override decimal PaymentAmount
        {
            get { return RelatedEntity.GetSurchargeValue(Payment.SurchargeId); }
        }

        public override ActionResult PostPaymentAction
        {
            get { return MVC.Thread.ThreadDetails(Payment.RelatedId); }
        }

        public static new string StrategyName { get { return typeof(ThreadSurchargeStrategy).FullName; } }

        public static new int StrategyId { get { return 2; } }

        public override void BeforePayment()
        {
        }

        public override void AfterPayment()
        {
            if (Payment.Status == PaymentStatus.Failed)
                EventLog.Event<PaymentFailedEvent>(Payment);

            #region Aktualizacja wątku

            //Zasilenie konta nową gotówką
            if (Payment.Amount > decimal.Zero)
                RelatedService.AddTransfer(
                    Transfer.Cash(Payment.Amount,
                                  string.Format(Resources.Payment.TransferCashThreadPayment, RelatedService.Name), Payment),
                    Payment.User);
            //Pobranie dopłaty za usługę
            RelatedService.AddTransfer(
                Transfer.Pending(-PaymentAmount,
                                 string.Format(Resources.Payment.TransferPendingThreadPayment, RelatedService.Name)),
                Payment.User);

            var thread = RelatedEntity;

            //zwiększenie wartości wątku
            thread.Value += thread.GetSurchargeValue(Payment.SurchargeId);

            //przejęcie wątku przez eksperta, którego dopłata została zrealizowana
            thread.PriceProposals.First(p => p.Id == Payment.SurchargeId).Accepted = true;
            thread.State = ThreadState.Occupied;
            thread.Expert = thread.AcceptedPriceProposal.Expert;

            UpdateRelatedService();
            Repository.User.Update(Payment.User);

            #endregion

            Email.Send<ThreadPaymentConfirmationEmail>(RelatedEntity);
        }
    }
}
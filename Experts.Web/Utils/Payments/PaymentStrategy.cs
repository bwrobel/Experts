using System.Linq;
using System.Web.Mvc;
using Experts.Core.Entities;
using Experts.Core.Entities.Events;
using Experts.Core.Repositories;
using Experts.Web.Helpers;

namespace Experts.Web.Utils.Payments
{
    public abstract class PaymentStrategy
    {
        public abstract decimal PaymentAmount { get; }

        public virtual void BeforePayment()
        {
        }

        public virtual void AfterPayment()
        {
        }

        public abstract ActionResult PostPaymentAction { get; }
        public abstract void UpdateRelatedService();

        public RepositoryFactory Repository { get; set; }
        public UrlHelper Url { get; set; }
        public Payment Payment { get; set; }

        public abstract IService RelatedService { get; }

        public static string GetStrategyDisplayName(string strategyName)
        {
            var nameParts = strategyName.Split('.');
            var shortName = nameParts.Last();
            return Resources.Payment.ResourceManager.GetString(shortName);
        }
    }

    public abstract class PaymentStrategy<TService> : PaymentStrategy
        where TService : class, IService
    {
        protected abstract TService GetRelatedService(int relatedId);
        public override IService RelatedService { get { return RelatedEntity; } }
        protected TService RelatedEntity { get { return _relatedEntity ?? (_relatedEntity = GetRelatedService(Payment.RelatedId)); } }
        private TService _relatedEntity;

        public override void AfterPayment()
        {
            if (Payment.Status == PaymentStatus.Failed)
                EventLog.Event<PaymentFailedEvent>(Payment);
        }
    }
}
namespace Experts.Web.Models.Payments
{
    public class PaymentModel
    {
        public int StrategyId { get; set; }
        public decimal? Value { get; set; }
        public int? RelatedId { get; set; }
        public int? SurchargeId { get; set; }
    }
}
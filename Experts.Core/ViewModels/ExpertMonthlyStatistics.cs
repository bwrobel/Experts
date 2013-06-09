namespace Experts.Core.ViewModels
{
    public class ExpertMonthlyStatistics
    {
        public int AcceptedAnswersPerMonth { get; set; }

        public int PositiveFeedbackPerMonth { get; set; }
        public int NeutralFeedbackPerMonth { get; set; }
        public int NegativeFeedbackPerMonth { get; set; }

        public decimal Balance { get; set; }
    }
}
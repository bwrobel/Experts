using Experts.Core.Entities;

namespace Experts.Core.ViewModels
{
    public class FeedbackStatistic
    {
        public int Count { get; set; }
        public int Month { get; set; }
        public FeedbackMark Mark { get; set; }
    }

    public class PaymentStatistic
    {
        public decimal Value { get; set; }
        public int Month { get; set; }

        public int IntState { get; set; }
        public PaymentStatisticState State
        {
            get { return (PaymentStatisticState) IntState; }
            set {IntState = (int)value;}
        }

        public enum PaymentStatisticState
        {
            Bought = 1,
            Sold = 2
        }
    }

    public class AnswerStatistic
    {
        public int Count { get; set; }
        public int Month { get; set; }
        public AnswerStatisticState State { get { return (AnswerStatisticState)IntState; } set { IntState = (int)value; } }
        public int IntState { get; set; }

        public enum AnswerStatisticState
        {
            Occupied = 1,
            GivenUp = 2,
            Accepted = 3
        }
    }
}

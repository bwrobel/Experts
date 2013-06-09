using System.Collections.Generic;
using Experts.Core.Entities;

namespace Experts.Core.ViewModels
{
    public class ExpertOverviewCharts
    {
        public IEnumerable<FeedbackStatistic> FeedbackStatistic { get; set; }
        public IEnumerable<AnswerStatistic> AnswerStatistic { get; set; }
        public IEnumerable<PaymentStatistic> PaymentStatistic { get; set; }
    }
}
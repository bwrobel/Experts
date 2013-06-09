using System.Collections.Generic;
using Experts.Core.Entities;

namespace Experts.Core.ViewModels
{
    public class ExpertOverviewViewModel
    {
        public int Id { get; set; }

        public User User { get; set; }

        public string PublicName { get; set; }

        public string PhoneNumber { get; set; }

        public string Position { get; set; }
        public string Description { get; set; }

        public IEnumerable<Thread> Threads { get; set; } 

        public IEnumerable<Feedback> Feedbacks { get; set; }
        public bool AreFeedbacksVisible { get; set; }

        public int PositiveCount { get; set; }
        public int NeutralCount { get; set; }
        public int NegativeCount { get; set; }
        public double PositivePercentage { get; set; }
        public int AcceptedAnswers { get; set; }

        public bool IsVerified { get; set; }

        public string Category { get; set; }

        public int ThreadCategoryId { get; set; }

        public int CategoryId { get; set; }

        public bool IsCommentEnabled { get; set; }

        public ExpertOverviewCharts ExpertOverviewCharts { get; set; }

        public int AcceptedAnswersPerMonth { get; set; }

        public string VerificationDetails { get; set; }

        public string BoxTitle { get; set; }
        public bool IsEmbedded { get; set; }  

        public bool ShowVerificationDetails { get; set; }
    }
}
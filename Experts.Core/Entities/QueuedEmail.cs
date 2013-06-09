using System;

namespace Experts.Core.Entities
{
    public class QueuedEmail:IEntity
    {
        public QueuedEmail()
        {
            SendTime = DateTime.Now;
        }

        public int Id { get; set; }
        public virtual EmailData Data { get; set; }

        public QueuedEmailStatus Status
        {
            get { return (QueuedEmailStatus)IntStatus; }
            set { IntStatus = (int)value; }
        }
        
        public int IntStatus { get; set; }
        public DateTime SendTime { get; set; }
    }

    public enum QueuedEmailStatus
    {
        Queued = 0,
        Sending = 1,
        Sended = 3,
        ReQueued = 4,
        NotToSend = 5
    }

    [Flags]
    public enum EmailType : long
    {
        OccupyThreadEmail = 1,
        GiveUpEmail = 2,
        NewInterestingThreadEmail = 4,
        PriceProposalEmail = 8,
        ThreadMoreDetailsEmail = 16,
        ThreadAnsweredEmail = 32,
        AdditionalServiceEmail = 64,
        ActivationEmail = 128,
        PasswordForgottenEmail = 256,
        ConfirmationEmail = 512,
        DirectQuestionEmail = 1024,
        ThreadAnswerAcceptedEmail = 2048,
        PriceProposalAcceptedEmail = 4096,
        ThreadNewPostToExpertEmail = 8192,
        AdditionalServiceAnswerEmail = 16384,
        Newsletter = 32768,
        ChatSummary = 65536,
        ThreadPaymentConfirmationEmail = 131072,
        ServicePaymentConfirmationEmail = 262144,
        ThreadUserAddAttachmentEmail = 524288,
        ThreadExpertAddAttachmentEmail = 1048576,
        FeedbackCommentEmail = 2097152,
        NewExpertEmail = 4194304,
        AwaitingExpertResponseEmail = 8388608,
        ExpertStatisticsEmail = 16777216,
        AcceptAnswerReminder = 33554432,
        NoExpertResponse = 67108864,
        MonthlyAskEncouragement = 134217728,
        UserPayoffConfirmationEmail = 268435456,
        PartnerStatisticsEmail = 536870912,
        NewModeratorPostUserEmail = 1073741824,
        NewModeratorPostExpertEmail = 2147483648,
        NewPMToExpertEmail = 4294967296,
        RecommendationEmail = 8589934592,
        RecommenderEmail = 17179869184,
        RecommenderNotRegisteredEmail = 34359738368,
        PartnerAcceptedEmail = 68719476736,
        PartnerRejectedEmail = 137438953472
    }
}

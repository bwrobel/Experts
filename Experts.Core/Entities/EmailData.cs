using Experts.Core.Repositories;
using Experts.Core.Utils;
using Experts.Core.ViewModels;
using Resources;
using System;

namespace Experts.Core.Entities
{
    public abstract class EmailData : IEntity
    {
        public int Id { get; set; }
        public abstract string RecipientEmail { get; }
        public abstract string Subject { get; }
        public abstract string TemplateName { get; }
        public abstract EmailType EmailType { get; }
        public abstract bool HasPermissionToBeSent { get; }
        public virtual bool IsObligatory { get { return false; } }
        public abstract EmailRecipientType RecipientType { get; }
    }

    [Flags]
    public enum EmailRecipientType
    {
        User = 1,
        Expert = 2,
        Partner = 3,
        NotSpecified = 4
    }

    public abstract class UserRecipientEmailData : EmailData
    {
        public abstract User RecipientUser { get; }
        public override string RecipientEmail { get { return RecipientUser.Email; } }
        public override bool HasPermissionToBeSent { get { return IsObligatory || RecipientUser.EmailConfiguration.HasFlag(EmailType); } }
    }

    public abstract class BaseThreadEmail : UserRecipientEmailData
    {
        public virtual Thread RelatedThread { get; set; }
        public override string Subject { get { return string.Format(Email.ThreadQuestionSubject, RelatedThread.Name); } }
        public abstract string Message { get; }
        public override string TemplateName { get { return "ThreadEmail"; } }
    }

    public abstract class ExpertEmail : UserRecipientEmailData
    {
        public virtual Expert RelatedExpert { get; set; }
        public virtual ExpertMonthlyStatistics RelatedExpertMonthlyStatistics { get; set; }
        public virtual string MonthName { get; set; }
        public override string Subject { get { return Email.ExpertStatisticsEmailSubject; } }
        public abstract string Message { get; }
        public override string TemplateName { get { return "ExpertEmail"; } }
    }

    public abstract class PartnerEmail : UserRecipientEmailData
    {
        public virtual Partner RelatedPartner { get; set; }
        public override User RecipientUser { get { return RelatedPartner.User; } }
        public override EmailRecipientType RecipientType { get { return EmailRecipientType.Partner; } }
    }

    public abstract class ThreadAdditionalServiceEmail : UserRecipientEmailData
    {
        public virtual AdditionalService RelatedAdditionalService { get; set; }
        public override string Subject { get { return string.Format(Email.AdditionalServiceEmailSubject, RelatedAdditionalService.Title); } }
        public abstract string Message { get; }
        public override string TemplateName { get { return "AdditionalServiceEmail"; } }
    }

    public abstract class ThreadEmail : BaseThreadEmail
    {
    }

    public abstract class ThreadExpertEmail : BaseThreadEmail
    {
        public virtual Expert RelatedExpert { get; set; }
    }

    public abstract class ThreadPriceProposalsEmail : BaseThreadEmail
    {
        public virtual string PriceProposals { get; set; }
    }

    public abstract class UserEmail : UserRecipientEmailData
    {
        public virtual User RelatedUser { get; set; }
    }

    public abstract class RecommendEmail : EmailData
    {
        public virtual Recommendation RelatedRecommendation { get; set; }
    }


    public class OccupyThreadEmail : ThreadEmail
    {
        public override User RecipientUser { get { return RelatedThread.Author; } }
        public override string Message { get { return string.Format(Email.ThreadOccupyEmailContent, RelatedThread.Expert.PublicName); } }
        public override EmailType EmailType { get { return EmailType.OccupyThreadEmail; } }
        public override EmailRecipientType RecipientType { get { return EmailRecipientType.User; } }
    }

    public class AdditionalServiceEmail : ThreadAdditionalServiceEmail
    {
        public override User RecipientUser { get { return RelatedAdditionalService.Thread.Author; } }
        public override string Message { get { return string.Format(Email.AdditionalServiceEmailContent, RelatedAdditionalService.Expert.PublicName); } }
        public override EmailType EmailType { get { return EmailType.AdditionalServiceEmail; } }
        public override EmailRecipientType RecipientType { get { return EmailRecipientType.User; } }
    }

    public class AdditionalServiceAnswerEmail : ThreadAdditionalServiceEmail
    {
        public override User RecipientUser { get { return RelatedAdditionalService.Expert.User; } }
        public override string Message { get { return Email.AdditionalServiceAnswerEmailContent; } }
        public override EmailType EmailType { get { return EmailType.AdditionalServiceAnswerEmail; } }
        public override EmailRecipientType RecipientType { get { return EmailRecipientType.User; } }
    }

    public class AwaitingExpertResponseEmail : ThreadEmail
    {
        public override User RecipientUser { get { return RelatedThread.Author; } }
        public override string Message { get { return Email.AwaitingExpertResponseEmailContent; } }
        public override EmailType EmailType { get { return EmailType.AwaitingExpertResponseEmail; } }
        public override EmailRecipientType RecipientType { get { return EmailRecipientType.User; } }
    }

    public class AcceptAnswerReminderEmail : ThreadEmail
    {
        public override User RecipientUser { get { return RelatedThread.Author; } }
        public override string Message { get { return string.Format(Email.AcceptAnswerReminderContent, RelatedThread.Expert.PublicName); } }
        public override EmailType EmailType { get { return EmailType.AcceptAnswerReminder; } }
        public override EmailRecipientType RecipientType { get { return EmailRecipientType.User; } }
    }

    public class NoExpertResponseEmail : ThreadPriceProposalsEmail
    {
        public override User RecipientUser { get { return RelatedThread.Author; } }
        public override string Message { get { return string.Format(Email.NoExpertResponseContent, PriceProposals ); } }
        public override EmailType EmailType { get { return EmailType.NoExpertResponse; } }
        public override EmailRecipientType RecipientType { get { return EmailRecipientType.User; } }
    }

    public class GiveUpEmail : ThreadExpertEmail
    {
        public override User RecipientUser { get { return RelatedThread.Author; } }
        public override string Message { get { return string.Format(Email.ThreadGiveUpContent, RelatedExpert.PublicName); } }
        public override EmailType EmailType { get { return EmailType.GiveUpEmail; } }
        public override EmailRecipientType RecipientType { get { return EmailRecipientType.User; } }
    }

    public class PriceProposalEmail : ThreadExpertEmail
    {
        public override User RecipientUser { get { return RelatedThread.Author; } }
        public override string Message { get { return string.Format(Email.ThreadPriceProposalContent, RelatedExpert.PublicName); } }
        public override EmailType EmailType { get { return EmailType.PriceProposalEmail; } }
        public override EmailRecipientType RecipientType { get { return EmailRecipientType.User; } }
    }

    public class ExpertStatisticsEmail : ExpertEmail
    {
        public override User RecipientUser { get { return RelatedExpert.User  ; } }
        public override string Message { get { return string.Format(Email.ExpertStatisticsEmailContent, 
            MonthName, 
            RelatedExpertMonthlyStatistics.AcceptedAnswersPerMonth,
            RelatedExpertMonthlyStatistics.PositiveFeedbackPerMonth,
            RelatedExpertMonthlyStatistics.NeutralFeedbackPerMonth,
            RelatedExpertMonthlyStatistics.NegativeFeedbackPerMonth,
            RelatedExpertMonthlyStatistics.Balance); } }
        public override EmailType EmailType { get { return EmailType.ExpertStatisticsEmail; } }
        public override EmailRecipientType RecipientType { get { return EmailRecipientType.Expert; } }
    }

    public class PartnerStatisticsEmail : PartnerEmail
    {
        public override string Subject { get { return Email.PartnerStatisticsEmailSubject; } }
        public override string TemplateName { get { return "PartnerStatisticsEmail"; } }
        public override EmailType EmailType { get { return EmailType.PartnerStatisticsEmail; } }
        public PartnerMonthlyStatistics PartnerMonthlyStatistics { get { return RepositoryHelper.Repository.Partner.GetPartnerMonthlyStatistics(RelatedPartner); } }
    }

    public class ThreadMoreDetailsEmail : ThreadEmail
    {
        public override User RecipientUser { get { return RelatedThread.Author; } }
        public override string Message { get { return string.Format(Email.ThreadMoreDetailsEmailContent, RelatedThread.Expert.PublicName); } }
        public override EmailType EmailType { get { return EmailType.ThreadMoreDetailsEmail; } }
        public override EmailRecipientType RecipientType { get { return EmailRecipientType.User; } }
    }

    public class ThreadAnsweredEmail : ThreadEmail
    {
        public override User RecipientUser { get { return RelatedThread.Author; } }
        public override string Message { get { return string.Format(Email.ThreadNewPostToUserEmailContent, RelatedThread.Expert.PublicName); } }
        public override EmailType EmailType { get { return EmailType.ThreadAnsweredEmail; } }
        public override EmailRecipientType RecipientType { get { return EmailRecipientType.User; } }
    }


    public class ThreadUserAddAttachmentEmail : ThreadEmail
    {
        public override User RecipientUser { get { return RelatedThread.Expert.User; } }
        public override string Message { get { return Email.ThreadUserAddAttachmentEmailContent; } }
        public override EmailType EmailType { get { return EmailType.ThreadUserAddAttachmentEmail; } }
        public override EmailRecipientType RecipientType { get { return EmailRecipientType.Expert; } }
    }

    public class ThreadExpertAddAttachmentEmail : ThreadEmail
    {
        public override User RecipientUser { get { return RelatedThread.Author; } }
        public override string Message { get { return string.Format(Email.ThreadExpertAddAttachmentEmailContent, RelatedThread.Expert.PublicName); } }
        public override EmailType EmailType { get { return EmailType.ThreadExpertAddAttachmentEmail; } }
        public override EmailRecipientType RecipientType { get { return EmailRecipientType.User; } }
    }

    public class DirectQuestionEmail : ThreadEmail
    {
        public override User RecipientUser { get { return RelatedThread.Expert.User; } }
        public override string Message { get { return Email.ThreadDirectQuestionContent; } }
        public override EmailType EmailType { get { return EmailType.DirectQuestionEmail; } }
        public override EmailRecipientType RecipientType { get { return EmailRecipientType.Expert; } }
    }

    public class ThreadAnswerAcceptedEmail : ThreadEmail
    {
        public override User RecipientUser { get { return RelatedThread.Expert.User; } }
        public override string Message { get { return Email.ThreadAnswerAcceptedEmailContent; } }
        public override EmailType EmailType { get { return EmailType.ThreadAnswerAcceptedEmail; } }
        public override EmailRecipientType RecipientType { get { return EmailRecipientType.User; } }
    }

    public class PriceProposalAcceptedEmail : ThreadEmail
    {
        public override User RecipientUser { get { return RelatedThread.Expert.User; } }
        public override string Message { get { return Email.PriceProposalAcceptedEmailContent; } }
        public override EmailType EmailType { get { return EmailType.PriceProposalAcceptedEmail; } }
        public override EmailRecipientType RecipientType { get { return EmailRecipientType.User; } }
    }

    public class ThreadNewPostToExpertEmail : ThreadEmail
    {
        public override User RecipientUser { get { return RelatedThread.Expert.User; } }
        public override string Message { get { return Email.ThreadNewPostToExpertEmailContent; } }
        public override EmailType EmailType { get { return EmailType.ThreadNewPostToExpertEmail; } }
        public override EmailRecipientType RecipientType { get { return EmailRecipientType.User; } }
        public override bool HasPermissionToBeSent { get { return RelatedThread.Expert != null && base.HasPermissionToBeSent; } }

    }

    public class ThreadPaymentConfirmationEmail : ThreadEmail
    {
        public override User RecipientUser { get { return RelatedThread.Author; } }
        public override string Message { get { return Email.ThreadPaymentConfirmationContent; } }
        public override EmailType EmailType { get { return EmailType.ThreadPaymentConfirmationEmail; } }
        public override bool IsObligatory { get { return true; } }
        public override EmailRecipientType RecipientType { get { return EmailRecipientType.User; } }
    }

    public abstract class NewModeratorPostEmail : ThreadEmail
    {
        public override string Message { get { return Email.NewModeratorPostEmailContent; } }
    }

    public class NewModeratorPostUserEmail : NewModeratorPostEmail
    {
        public override EmailType EmailType { get { return EmailType.NewModeratorPostUserEmail; } }
        public override EmailRecipientType RecipientType { get { return EmailRecipientType.User; } }
        public override User RecipientUser { get { return RelatedThread.Author; } }
    }

    public class NewModeratorPostExpertEmail : NewModeratorPostEmail
    {
        public override EmailType EmailType { get { return EmailType.NewModeratorPostExpertEmail; } }
        public override EmailRecipientType RecipientType { get { return EmailRecipientType.Expert; } }
        public override User RecipientUser { get { return RelatedThread.Expert.User; } }
    }

    public class NewPMToExpertEmail : ThreadEmail
    {
        public override EmailType EmailType { get { return EmailType.NewPMToExpertEmail; } }
        public override EmailRecipientType RecipientType { get { return EmailRecipientType.Expert; } }
        public override User RecipientUser { get { return RelatedThread.Expert.User; } }
        public override string Message { get { return Email.NewPMToExpertEmailContent; } }
    }

    public class FeedbackCommentEmail : ThreadEmail
    {
        public override User RecipientUser { get { return RelatedThread.Author; } }
        public override string Subject { get { return Email.FeedbackCommentEmailSubject; } }
        public override string Message { get { return string.Format(Email.FeedbackCommentEmailContent, RelatedThread.Expert.PublicName); } }
        public override EmailType EmailType { get { return EmailType.FeedbackCommentEmail; } }
        public override EmailRecipientType RecipientType { get { return EmailRecipientType.Expert; } }
    }


    public class NewExpertEmail : UserEmail
    {
        public override User RecipientUser { get { return RelatedUser; } }
        public override string Subject { get { return Email.NewExpertEmailSubject; } }
        public override string TemplateName { get { return "NewExpertEmail"; } }
        public override EmailType EmailType { get { return EmailType.NewExpertEmail; } }
        public override bool IsObligatory { get { return true; } } 
        public override EmailRecipientType RecipientType { get { return EmailRecipientType.Expert; } }
    }

    public class MonthlyAskEncouragementEmail : UserEmail
    {   
        public override User RecipientUser { get { return RelatedUser; } }
        public override string Subject { get { return Email.MonthlyAskEncouragementSubject; } }
        public override string TemplateName { get { return "MonthlyAskEncouragement"; } }
        public override EmailType EmailType { get { return EmailType.MonthlyAskEncouragement; } }
        public override EmailRecipientType RecipientType { get { return EmailRecipientType.User; } }
    }

    public class ActivationEmail : UserEmail
    {
        public override User RecipientUser { get { return RelatedUser; } }
        public override string Subject { get { return Email.ActivationEmailSubject; } }
        public override string TemplateName { get { return "ActivationEmail"; } }
        public override EmailType EmailType { get { return EmailType.ActivationEmail; } }
        public override bool IsObligatory { get { return true; } }
        public override EmailRecipientType RecipientType { get { return EmailRecipientType.User; } }
    }

    public class PasswordForgottenEmail : UserEmail
    {
        public override User RecipientUser { get { return RelatedUser; } }
        public override string Subject { get { return Email.PasswordForgottenEmailSubject; } }
        public override string TemplateName { get { return "PasswordForgottenEmail"; } }
        public override EmailType EmailType { get { return EmailType.PasswordForgottenEmail; } }
        public override bool IsObligatory { get { return true; } }
        public override EmailRecipientType RecipientType { get { return EmailRecipientType.User; } }
    }

    public class ConfirmationEmail : UserEmail
    {
        public override string RecipientEmail { get { return RecipientUser.NewEmail; } }
        public override User RecipientUser { get { return RelatedUser; } }
        public override string Subject { get { return Email.EmailConfirmationEmailSubject; } }
        public override string TemplateName { get { return "ConfirmationEmail"; } }
        public override EmailType EmailType { get { return EmailType.ConfirmationEmail; } }
        public override bool IsObligatory { get { return true; } }
        public override EmailRecipientType RecipientType { get { return EmailRecipientType.User; } }
    }

    public class RecommendationEmail : RecommendEmail
    {
        public override string RecipientEmail { get { return RelatedRecommendation.RecommendedExpertEmail; } }
        public override string Subject { get { return Email.RecommendationSubject; } }
        public override string TemplateName { get { return "RecommendationEmail"; } }
        public override EmailType EmailType { get { return EmailType.RecommendationEmail; } }
        public override bool HasPermissionToBeSent { get { return true; } }
        public override EmailRecipientType RecipientType { get { return EmailRecipientType.User; } }
        public override bool IsObligatory { get { return true; } }
    }

    public class RecommenderNotRegisteredEmail : RecommendEmail
    {
        public override string RecipientEmail { get { return RelatedRecommendation.RecommenderEmail; } }
        public override string Subject { get { return Email.RecommenderNotRegisteredSubject; } }
        public override string TemplateName { get { return "RecommenderNotRegisteredEmail"; } }
        public override EmailType EmailType { get { return EmailType.RecommenderNotRegisteredEmail; } }
        public override bool HasPermissionToBeSent { get { return true; } }
        public override EmailRecipientType RecipientType { get { return EmailRecipientType.User; } }
        public override bool IsObligatory { get { return true; } }
    }

    public class RecommenderEmail : UserEmail
    {
        public override User RecipientUser { get { return RelatedUser; } }
        public override string Subject { get { return Email.RecommenderSubject; } }
        public override string TemplateName { get { return "RecommenderEmail"; } }
        public override EmailType EmailType { get { return EmailType.RecommenderEmail; } }
        public override bool IsObligatory { get { return true; } }
        public override EmailRecipientType RecipientType { get { return EmailRecipientType.User; } }
    }

    public class ChatSummaryEmail : EmailData
    {
        public virtual Chat Chat { get; set; }
        public override string RecipientEmail { get { return Chat.OwnerEmail ?? Chat.Owner.Email; } }
        public override string Subject { get { return Email.ChatSummarySubject; } }
        public override string TemplateName { get { return "ChatSummaryEmail"; } }
        public override EmailType EmailType { get { return EmailType.ConfirmationEmail; } }
        public override bool HasPermissionToBeSent { get { return Chat.Owner == null || Chat.Owner.EmailConfiguration.HasFlag(EmailType); } }
        public override EmailRecipientType RecipientType { get { return EmailRecipientType.User; } }
    }


    public class NewInterestingThreadEmail : ThreadExpertEmail
    {
        public override User RecipientUser { get { return RelatedExpert.User; } }
        public override string Subject { get { return Email.NewInterestingThreadSubject; } }
        public override string Message { get { return string.Format(Email.NewInterestingEmailContent, RelatedThread.Name); } }
        public override string TemplateName { get { return "NewInterestingThreadEmail"; } }
        public override EmailType EmailType { get { return EmailType.NewInterestingThreadEmail; } }
        public override EmailRecipientType RecipientType { get { return EmailRecipientType.Expert; } }
    }


    public class ServicePaymentConfirmationEmail : ThreadEmail
    {
        public override User RecipientUser { get { return RelatedThread.Author; } }
        public override string Message { get { return Email.ServicePaymentConfirmationContent; } }
        public override EmailType EmailType { get { return EmailType.ServicePaymentConfirmationEmail; } }
        public override bool IsObligatory { get { return true; } }
        public override EmailRecipientType RecipientType { get { return EmailRecipientType.User; } }
    }

    public class UserPayoffConfirmationEmail : UserEmail
    {
        public override User RecipientUser { get { return RelatedUser; } }
        public override string Subject { get { return Email.UserPayoffConfirmationSubject; } }
        public override string TemplateName { get { return "UserPayoffConfirmationEmail"; } }
        public override EmailType EmailType { get { return EmailType.UserPayoffConfirmationEmail; } }
        public override EmailRecipientType RecipientType { get { return EmailRecipientType.User; } }
    }

    public class PartnerAcceptedEmail : UserEmail
    {
        public override User RecipientUser { get { return RelatedUser; } }
        public override string Subject { get { return Email.PartnerAcceptedSubject; } }
        public override string TemplateName { get { return "PartnerAcceptedEmail"; } }
        public override EmailType EmailType { get { return EmailType.PartnerAcceptedEmail; } }
        public override EmailRecipientType RecipientType { get { return EmailRecipientType.User; } }
    }

    public class PartnerRejectedEmail : UserEmail
    {
        public override User RecipientUser { get { return RelatedUser; } }
        public override string Subject { get { return Email.PartnerRejectedSubject; } }
        public override string TemplateName { get { return "PartnerRejectedEmail"; } }
        public override EmailType EmailType { get { return EmailType.PartnerRejectedEmail; } }
        public override EmailRecipientType RecipientType { get { return EmailRecipientType.User; } }
    }
}

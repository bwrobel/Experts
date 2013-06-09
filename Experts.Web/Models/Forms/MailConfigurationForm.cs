using System.Collections.Generic;
using System.Linq;
using Experts.Core.Entities;
using Experts.Core.Utils;

namespace Experts.Web.Models.Forms
{
    public class MailConfigurationForm
    {
        //[Display(Name = Resources.EmailConfigurationsConstants.OccupyThreadEmail, ResourceType = typeof(Resources.EmailConfigurations))]
        //public bool OccupyThreadEmail { get; set; }

        //[Display(Name = Resources.EmailConfigurationsConstants.GiveUpEmail, ResourceType = typeof(Resources.EmailConfigurations))]
        //public bool GiveUpEmail { get; set; }

        //[Display(Name = Resources.EmailConfigurationsConstants.ReportIssueEmail, ResourceType = typeof(Resources.EmailConfigurations))]
        //public bool ReportIssueEmail { get; set; }

        //[Display(Name = Resources.EmailConfigurationsConstants.PriceProposalEmail, ResourceType = typeof(Resources.EmailConfigurations))]
        //public bool PriceProposalEmail { get; set; }

        //[Display(Name = Resources.EmailConfigurationsConstants.ThreadMoreDetailsEmail, ResourceType = typeof(Resources.EmailConfigurations))]
        //public bool ThreadMoreDetailsEmail { get; set; }

        //[Display(Name = Resources.EmailConfigurationsConstants.ThreadNewPostEmailToUser, ResourceType = typeof(Resources.EmailConfigurations))]
        //public bool ThreadNewPostEmailToUser { get; set; }

        //[Display(Name = Resources.EmailConfigurationsConstants.DirectQuestionEmail, ResourceType = typeof(Resources.EmailConfigurations))]
        //public bool DirectQuestionEmail { get; set; }

        //[Display(Name = Resources.EmailConfigurationsConstants.ThreadAnswerAcceptedEmail, ResourceType = typeof(Resources.EmailConfigurations))]
        //public bool ThreadAnswerAcceptedEmail { get; set; }

        //[Display(Name = Resources.EmailConfigurationsConstants.PaymentAnswerEmail, ResourceType = typeof(Resources.EmailConfigurations))]
        //public bool PaymentAnswerEmail { get; set; }

        //[Display(Name = Resources.EmailConfigurationsConstants.ThreadNewPostEmailToExpert, ResourceType = typeof(Resources.EmailConfigurations))]
        //public bool ThreadNewPostEmailToExpert { get; set; }

        //[Display(Name = Resources.EmailConfigurationsConstants.AdditionalServiceEmail, ResourceType = typeof(Resources.EmailConfigurations))]
        //public bool AdditionalServiceEmail { get; set; }

        //[Display(Name = Resources.EmailConfigurationsConstants.AdditionalServiceAnswerEmail, ResourceType = typeof(Resources.EmailConfigurations))]
        //public bool AdditionalServiceAnswerEmail { get; set; }

        //[Display(Name = Resources.EmailConfigurationsConstants.Newsletter, ResourceType = typeof(Resources.EmailConfigurations))]
        //public bool Newsletter { get; set; }

        public static MailConfigurationForm Create(EmailType selectedValues, EmailRecipientType recipientTypes)
        {
            var configuration = new MailConfigurationForm { RecipientTypes = recipientTypes, SelectedEmails = new Dictionary<EmailType, bool>() };
            var emailTypes = EmailMetadataHelper.GetTypesForRecipientType(recipientTypes);

            foreach (var emailType in emailTypes)
                configuration.SelectedEmails.Add(emailType, selectedValues.HasFlag(emailType));

            return configuration;
        }

        public Dictionary<EmailType, bool> SelectedEmails { get; set; }
        public EmailRecipientType RecipientTypes { get; set; }
    }
}
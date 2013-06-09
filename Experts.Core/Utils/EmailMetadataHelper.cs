using System.Collections.Generic;
using System.Linq;
using Experts.Core.Entities;
using System.Reflection;
using System;

namespace Experts.Core.Utils
{
    public static class EmailMetadataHelper
    {
        public static IEnumerable<EmailData> GetEmailTemplates()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var emailTypes = assembly.GetTypes().Where(t => t.IsSubclassOf(typeof(EmailData)) && !t.IsAbstract);
            return emailTypes.Select(emailType => (EmailData) Activator.CreateInstance(emailType));
        }

        public static IEnumerable<EmailType> GetTypesForRecipientType(EmailRecipientType recipientTypes)
        {
            return GetEmailTemplates().Where(e => !e.IsObligatory && recipientTypes.HasFlag(e.RecipientType)).Select(e => e.EmailType);
        }
    }
}

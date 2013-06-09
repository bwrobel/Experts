using System;
using System.IO;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;
using System.Threading;

namespace Experts.Specs.Helpers
{
    static class MailHelper
    {
        public static string GetByRecipientAndSubject(string recipientEmail, string subject)
        {
            Thread.Sleep(2000);

            var subjectRegex = new Regex("Subject:.+" + subject + ".+");   
            var toRegex = new Regex("To: " + recipientEmail);   

            var lastEmailFiles = Directory.GetFiles(ConfigurationManager.AppSettings["mailDirectory"])
                .Where(fname => File.GetLastWriteTime(fname) > DateTime.Now - TimeSpan.FromMinutes(5));

            foreach (var lastEmailFile in lastEmailFiles)
            {
                var fileContents = File.ReadAllText(lastEmailFile);
                if (subjectRegex.IsMatch(fileContents) && toRegex.IsMatch(fileContents))
                    return GetEmailContents(fileContents);
            }

            return null;
        }

        public static string GetByRecipient(string recipientEmail)
        {
            Thread.Sleep(2000);

            var toRegex = new Regex("To: " + recipientEmail);

            var lastEmailFiles = Directory.GetFiles(ConfigurationManager.AppSettings["mailDirectory"])
                .Where(fname => File.GetLastWriteTime(fname) > DateTime.Now - TimeSpan.FromSeconds(10));

            foreach (var lastEmailFile in lastEmailFiles)
            {
                var fileContents = File.ReadAllText(lastEmailFile);
                if (toRegex.IsMatch(fileContents))
                    return GetEmailContents(fileContents);
            }

            return null;
        }

        private static string GetEmailContents(string fileContents)
        {
            fileContents = fileContents.Trim();
            var encodedContents = fileContents.Substring(fileContents.IndexOf(Environment.NewLine + Environment.NewLine));
            return Encoding.UTF8.GetString(Convert.FromBase64String(encodedContents));
        }
    }
}
 
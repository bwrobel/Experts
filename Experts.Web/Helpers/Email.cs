using System;
using System.Collections.Generic;
using System.Web;
using Experts.Core.Entities;
using Experts.Core.Utils;
using System.Net.Mail;
using Experts.Core.ViewModels;
using System.Web;

namespace Experts.Web.Helpers
{
    public static class Email
    {
        private static bool _isSending;

        public static void Send<T>(Thread thread)
            where T: ThreadEmail, new()
        {
            var emailData = new T {RelatedThread = thread};
            Send(emailData);
        }

        public static void Send<T>(Thread thread, Expert expert)
          where T : ThreadExpertEmail, new()
        {
            var emailData = new T { RelatedThread = thread, RelatedExpert = expert };
            Send(emailData);
        }

        public static void Send<T>(User user)
          where T : UserEmail, new()
        {
            var emailData = new T { RelatedUser = user };
            Send(emailData);
        }

        public static void Send<T>(Expert expert, string monthName, ExpertMonthlyStatistics expertMonthlyStatistics)
          where T : ExpertEmail, new()
        {
            var emailData = new T { RelatedExpert = expert, MonthName = monthName, RelatedExpertMonthlyStatistics = expertMonthlyStatistics};
            Send(emailData);
        }

        public static void Send<T>(Thread thread, string priceProposals)
          where T : ThreadPriceProposalsEmail, new()
        {
            var emailData = new T { RelatedThread = thread, PriceProposals = priceProposals};
            Send(emailData);
        }

        public static void Send<T>(Recommendation recommendation)
            where  T:RecommendEmail, new()
        {
            var emailData = new T {RelatedRecommendation = recommendation};
            Send(emailData);
        }

        public static void Send<T>(AdditionalService additionalService)
            where T : ThreadAdditionalServiceEmail, new()
        {
            var emailData = new T { RelatedAdditionalService = additionalService };
            Send(emailData);
        }

        public static void Send<T>(Chat chat)
          where T : ChatSummaryEmail, new()
        {
            var emailData = new T { Chat = chat };
            Send(emailData);
        }

        public static void Send<T>(Partner partner)
            where T : PartnerEmail, new()
        {
            var emailData = new T {RelatedPartner = partner};
            Send(emailData);
        }

        private static void Send(EmailData data)
        {
            var email = new QueuedEmail { Data = data };
            RepositoryHelper.Repository.QueuedEmail.Add(email);
            new System.Threading.Thread(RunMailQueue).Start();
        }

        public static void RunMailQueue()
        {
            if (_isSending)
            {
                return;
            }

            _isSending = true;

            try
            {
                var repository = RepositoryHelper.Repository;

                QueuedEmail queuedEmail;
                while ((queuedEmail = repository.QueuedEmail.GetFirstToSend()) != null)
                {
                    queuedEmail.Status = QueuedEmailStatus.Sending;
                    repository.QueuedEmail.Update(queuedEmail);

                    try
                    {
                        var result = SendEmail(queuedEmail);

                        queuedEmail.Status = result ? QueuedEmailStatus.Sended : QueuedEmailStatus.NotToSend;
                        repository.QueuedEmail.Update(queuedEmail);
                    }
                    catch (Exception ex)
                    {
                        queuedEmail.SendTime = DateTime.Now + TimeSpan.FromMinutes(5);
                        queuedEmail.Status = QueuedEmailStatus.ReQueued;

                        repository.QueuedEmail.Update(queuedEmail);

                        ErrorsHelper.LogApplicationException(ex);
                    }
                }
            }
            catch(Exception ex)
            {
                ErrorsHelper.LogApplicationException(ex);
            } 
            finally
            {
                _isSending = false;
            }
            
        }

        private static bool SendEmail(QueuedEmail queuedEmail)
        {
            if (!queuedEmail.Data.HasPermissionToBeSent) return false;

            var messageBody = RazorHelper.RenderMailTemplate(queuedEmail.Data) + Resources.Email.EmailFooter;
            var message = new MailMessage("kontakt@asknuts.com", queuedEmail.Data.RecipientEmail) { Body = HttpUtility.HtmlDecode(messageBody), IsBodyHtml = true, Subject = "AskNuts.com - " + queuedEmail.Data.Subject };
          
            var client = new SmtpClient();
            client.Send(message);

            return true;
        }

    }

}
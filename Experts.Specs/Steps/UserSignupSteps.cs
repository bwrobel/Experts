using Experts.Specs.Helpers;
using NUnit.Framework;
using TechTalk.SpecFlow;
using System.Text.RegularExpressions;

namespace Experts.Specs.Steps
{
    [Binding]
    public class UserSignupSteps
    {
        [When(@"kliknę w linka aktywacyjny użytkownika (.*)")]
        public void IfIClickOnActivationLinkForUser(string userEmail)
        {
            var emailContent = MailHelper.GetByRecipientAndSubject(userEmail, "AskNuts.com - " + Resources.Email.ActivationEmailSubject);
            
            const string pattern = @"/aktywuj-konto/(\w+)";
            var regex = new Regex(pattern);
            var match = regex.Match(emailContent);

            Assert.IsTrue(match.Success);

            var activationLink = match.Value;

            WebBrowser.Current.GoTo(UrlHelper.AbsoluteUrl(activationLink));
        }

    }
}

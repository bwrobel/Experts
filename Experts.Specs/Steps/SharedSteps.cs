using System.Text.RegularExpressions;
using Experts.Specs.Helpers;
using NUnit.Framework;
using TechTalk.SpecFlow;
using WatiN.Core;
using System.Linq;

namespace Experts.Specs.Steps
{
    [Binding]
    public class SharedSteps
    {
        [When(@"zaloguję się jako użytkownik")]
        [Given(@"że jestem zalogowany jako użytkownik")]
        public void LoggedAsUser()
        {
            AuthenticationHelper.LogInUser("user-asknuts@asknuts.com", "haslo1");
        }

        [When(@"zaloguję się jako moderator")]
        [Given(@"że jestem zalogowany jako moderator")]
        public void LoggedAsModerator()
        {
            AuthenticationHelper.LogInUser("moderator-asknuts@asknuts.com", "haslo1");
        }


        [When(@"zaloguję się jako ekspert")]
        [Given(@"że jestem zalogowany jako ekspert")]
        public void LoggedAsExpert()
        {
            AuthenticationHelper.LogInUser("expert-asknuts@asknuts.com", "haslo1");
        }

        
        [When(@"przejdę do (.*)")]
        public void WhenINavigateTo(string relativeUrl)
        {
            WebBrowser.Current.GoTo(UrlHelper.AbsoluteUrl(relativeUrl));
        }

        [When(@"wpiszę do pola (.*) tekst (.*)")]
        public void WhenITypeIntoField(string fieldLabel, string text)
        {
            var input = GetFieldByLabel(fieldLabel);
            input.SetAttributeValue("value", text);
        }

        [When(@"usunę z pola (.*) jego zawartość")]
        public void WhenIDeleteFieldContents(string fieldLabel)
        {
            var input = GetFieldByLabel(fieldLabel);
            input.SetAttributeValue("value", "");
        }

        [When("do pola tekstowego wpiszę tekst (.*)")]
        public void WhenITypeIntoTextField(string text)
        {
            var input = WebBrowser.Current.TextFields.FirstOrDefault(tf => tf.IsDisplayed());
            input.Value = text;
        }

        [When(@"wybiorę z listy (.*) pozycję (.*)")]
        public void WhenIPickFromTheList(string listLabel, string selectText)
        {
            var label = WebBrowser.Current.Label(Find.ByText(listLabel));
            var selectId = label.For;

            var element = WebBrowser.Current.SelectList(Find.ById(selectId));
            element.Select(selectText);

            WebBrowser.Current.InjectAjaxMonitor();
            WebBrowser.Current.Eval(string.Format("$('#{0}').change()", selectId));
            WebBrowser.Current.WaitForAjaxRequest();
        }

        [When(@"kliknę przycisk (.*)")]
        public void WhenIClickButton(string buttonText)
        {
            var button = WebBrowser.Current.Buttons.Filter(b => b.Value == buttonText && b.IsDisplayed()).FirstOrDefault();

            if (button != null && button.Exists)
            {
                button.Click();
                return;
            }

            var link = WebBrowser.Current.Links.Filter(Find.ByText(buttonText)).First(l => l.IsDisplayed());

            if (link != null && link.Exists)
            {
                link.Click();
                return;
            }

            var buttonById = WebBrowser.Current.Links.Filter(Find.ById(buttonText)).First(l => l.IsDisplayed());
            buttonById.Click();
        }

        [When(@"kliknę checkbox (.*)")]
        public void WhenIClickCheckbox(string checkboxLabel)
        {
            var checkbox = GetFieldByLabel(checkboxLabel) ??
                           WebBrowser.Current.CheckBox(c => c.Parent.OuterText.Contains(checkboxLabel));

            checkbox.Click();
        }

        [When(@"kliknę radio button (.*)")]
        public void WhenIClickRadioButton(string radioButtonLabel)
        {
            var radioButton = WebBrowser.Current.RadioButton(c => c.Parent.OuterText.Contains(radioButtonLabel));
            radioButton.Click();
        }

        [When(@"kliknę checkbox'a o Id (.*)")]
        public void WhenIClickCheckboxById(string checkboxId)
        {
            var checkbox = WebBrowser.Current.CheckBox(Find.ById(checkboxId));
            checkbox.Click();
        }

        [When(@"kliknę link w mailu wysłanym na adres (.*)")]
        public void WhenIClickLinkInEmail(string recipientEmail)
        {
            var emailContent = MailHelper.GetByRecipient(recipientEmail);

            const string pattern = @"https://[^\s<]+";
            var regex = new Regex(pattern);
            var match = regex.Match(emailContent);

            Assert.IsTrue(match.Success);

            var link = match.Value;

            WebBrowser.Current.GoTo(UrlHelper.AbsoluteUrl(link));
        }

        [When("odświeżę stronę")]
        public void WhenIRefreshPage()
        {
            WebBrowser.Current.Refresh();
        }

        [Then(@"zobaczę przycisk (.*)")]
        public void ThenISeeButton(string buttonText)
        {
            var button = WebBrowser.Current.Button(Find.ByValue(buttonText));
            var link = WebBrowser.Current.Link(Find.ByText(buttonText));

            Assert.IsTrue((button.Exists && button.IsDisplayed()) || (link.Exists && link.IsDisplayed()));
        }

        [Then(@"zobaczę checkbox'a o Id (.*), który ma status (.*)")]
        public void ThenIWillSeeUncheckedCheckboxById(string checkboxId, bool state)
        {
            var checkbox = WebBrowser.Current.CheckBox(Find.ById(checkboxId));

            Assert.IsTrue(checkbox.Checked == state);
        }

        [Then(@"zobaczę tekst (.*)")]
        public void ThenISeeText(string text)
        {
            Assert.IsTrue(IsTextVisible(text));
        }

        [Then(@"zobaczę w elemencie tekst (.*)")]
        public void ThenISeeTextOnPage(string text)
        {
            Assert.IsTrue(WebBrowser.Current.Element(Find.ByValue(text)).Exists);
        }

        [Then(@"zobaczę ikonę (.*)")]
        public void ThenISeeIcon(string icon)
        {
            Assert.IsTrue(WebBrowser.Current.Element(Find.ByClass(icon)).Exists);
        }

        [Then(@"nie zobaczę tekstu (.*)")]
        public void ThenIWontSeeText(string text)
        {
            Assert.IsFalse(IsTextVisible(text));
        }

        [Then(@"w polu (.*) zobaczę tekst (.*)")]
        public void ThenISeeTextInField(string fieldLabel, string text)
        {
            var input = GetFieldByLabel(fieldLabel);
            Assert.AreEqual(text, input.GetAttributeValue("value"));
        }

        [Then(@"znajdę się na stronie (.*)")]
        public void ThenILandOnPage(string path)
        {
            var url = WebBrowser.Current.Url;
            Assert.IsTrue(url.EndsWith(path));
        }

        [Then(@"znajdę się na zewnętrznej stronie (.*)")]
        public void ThenILandOnOuterPage(string path)
        {
            var url = WebBrowser.Current.Url;
            Assert.IsTrue(url == path);
        }

        [Then(@"otrzymam e-mail o temacie (.*) wysłany na adres (.*)")]
        public void ThenIReceiveEmail(string subject, string recipientEmail)
        {
            Assert.IsNotNull(MailHelper.GetByRecipientAndSubject(recipientEmail, subject));
        }

        [Then("zobaczę element iframe")]
        public void ThenISeeIframe()
        {
            Assert.IsTrue(WebBrowser.Current.Element(Find.BySelector("iframe")).Exists);
        }

        private Element GetFieldByLabel(string fieldLabel)
        {
            var label = WebBrowser.Current.Label(Find.ByText(fieldLabel));
            if (label.For == null)
                return null;

            return WebBrowser.Current.Elements.Filter(Find.ById(label.For)).FirstOrDefault(e => e.IsDisplayed());
        }

        private bool IsTextVisible(string text)
        {
            return WebBrowser.Current.ContainsText(text);
        }
    }
}

using Experts.Specs.Helpers;
using TechTalk.SpecFlow;
using WatiN.Core;

namespace Experts.Specs.Steps
{
    [Binding]
    public class ThreadsSanitizationSteps
    {
        [When("w poście do oczyszczenia o treści (.*) kliknę link (.*)")]
        public void WhenIClickLinkInSanitizedPost(string postText, string linkText)
        {
            var link = GetSanitizationPost(postText).Link(Find.ByText(linkText));
            link.Click();
        }

        [When("w poście do oczyszczenia o treści (.*) wpiszę tekst (.*)")]
        public void WhenITypeInSanitizedPost(string postText, string text)
        {
            var textarea = GetSanitizationPost(postText).TextField(Find.Any);
            textarea.Value = text;
        }

        private Div GetSanitizationPost(string text)
        {
            return WebBrowser.Current.Divs.Filter(Find.ByClass("post-sanitization", false)).First(p => p.Text.Contains(text));
        }
    }
}

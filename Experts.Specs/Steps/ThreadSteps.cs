using System.Linq;
using Experts.Specs.Helpers;
using NUnit.Framework;
using TechTalk.SpecFlow;
using WatiN.Core;

namespace Experts.Specs.Steps
{
    [Binding]
    public class ThreadSteps
    {
        [When("wpiszę treść pytania (.*)")]
        [When("wpiszę treść odpowiedzi (.*)")]
        public void WhenITypeQuestionText(string questionText)
        {
            var input = WebBrowser.Current.TextField("PostForm_Content");

            if (!input.Exists)
                input = WebBrowser.Current.TextField("ThreadForm_Content");

            Assert.IsTrue(input.Exists);

            input.Value = questionText;
        }

        [When("wybiorę kategorię pytania (.*) oraz wpiszę treść (.*)")]
        public void WhenIChooseCategoryAndTypeQuestionText(string categoryName, string questionText)
        {
            var shared = new SharedSteps();
            shared.WhenIClickButton(Resources.Thread.MoreCategories);
            shared.WhenIClickButton(categoryName);
            
            var input = WebBrowser.Current.TextField(Find.BySelector("textarea"));

            input.Value = questionText;
        }

        [When("w miniformie wybiorę kategorię pytania (.*)")]
        public void WhenIChooseCategoryInMiniForm(string categoryName)
        {
            var shared = new SharedSteps();
            shared.WhenIClickButton(Resources.Thread.TinyFormClickToChooseCategory);
            shared.WhenIClickButton(categoryName);
        }

        [When("w miniformie wpiszę treść (.*)")]
        public void WhenITypeQuestionTextIntoMiniform(string questionText)
        {
            var input = WebBrowser.Current.TextField(Find.BySelector("#question-form-tiny textarea"));
            input.Value = questionText;
        }

        [When("w poście o treści (.*) kliknę przycisk edycji")]
        public void WhenIClickEditPostButton(string postContent)
        {
            FindEditPostLink(postContent).Click();
        }

        [When("w poście o treści (.*) kliknę przycisk usunięcia")]
        public void WhenIClickDeletePostButton(string postContent)
        {
            var post = FindPost(postContent);
            post.Link(Find.ByUrl(u => u.Contains("/usun-wiadomosc/"))).Click();
        }

        [When("w poście o treści (.*) kliknę przycisk usunięcia załącznika")]
        public void WhenIClickDeleteAttachmentButton(string postContent)
        {
            var post = FindPost(postContent);
            post.Link(Find.ByUrl(u => u.Contains("#DeleteAttachmentModal"))).Click();
        }

        [When("prześlę załącznik (.*)")]
        public void WhenIUploadAttachment(string filename)
        {
            var upload = WebBrowser.Current.FileUpload(Find.Any);

            upload.Set(DataHelper.GetPath(filename));
            WebBrowser.Current.WaitUntilContainsText(filename);
        }

        [When(@"dodam załącznik (.*)")]
        public void WhenIAddAttachment(string filename)
        {
            var upload = WebBrowser.Current.FileUpload(Find.BySelector("input"));
            upload.Set(DataHelper.GetPath(filename));
            
            WebBrowser.Current.WaitForComplete();
        }

        [Then(@"zobaczę wykres")]
        public void ThenISeeCanvas()
        {
            Assert.IsTrue(WebBrowser.Current.Element(Find.BySelector("canvas")).Exists);
        }

        [Then(@"poczekam aż zobaczę tekst (.*)")]
        public void WillWaitUntilISeeText(string text)
        {
            WebBrowser.Current.WaitUntilContainsText(text);
        }

        [Then(@"pozostanę na stronie formularza płatności")]
        public void ThenILandOnPaymentFormPage()
        {
            var url = WebBrowser.Current.Url;
            Assert.IsTrue(url.Contains("/parametry-pytania"));
        }

        [Then("zobaczę post o treści (.*)")]
        public void ThenISeePost(string content)
        {
            Assert.IsNotNull(FindPost(content));
        }

        [Then("w poście o treści (.*) nie zobaczę przycisku edycji")]
        public void ThenIWontSeeEditPostButton(string postContent)
        {
            Assert.IsFalse(FindEditPostLink(postContent).Exists);
        }

        [Then("nie zobaczę żadnych przycisków edycji")]
        public void ThenIWontSeeAnyEditPostButtons()
        {
            Assert.False(WebBrowser.Current.Link(Find.ByUrl("javascript:void(0);")).Exists);
        }

        private static Div FindPost(string content)
        {
            var posts = WebBrowser.Current.Divs.Filter(Find.ByClass("post", false));
            return posts.Filter(p => p.Text.Contains(content)).FirstOrDefault();
        }

        private static Link FindEditPostLink(string postContent)
        {
            var post = FindPost(postContent);
            return post.Link(Find.ByUrl("javascript:void(0);"));
        }


        [Given(@"że ekspert dodał posta do przejętego pytania")]
        public void ExpertAddedPostToOccupiedQuestion()
        {
            var sharedSteps = new SharedSteps();

            sharedSteps.LoggedAsExpert();

            WebBrowser.Current.GoTo(UrlHelper.AbsoluteUrl("/pytania-uzytkownikow"));

            sharedSteps.WhenIClickButton("Pytanie testowe z ekspertem");
            sharedSteps.WhenIClickButton("Odpowiedz");

            WhenITypeQuestionText("Kolejna testowa odpowiedź");

            sharedSteps.WhenIClickButton("Dodaj odpowiedź");

            AuthenticationHelper.LogoutUser();
        }

        [Given(@"że użytkownik dodał posta do swojego pytania")]
        public void UserAddedPostToHisQuestion()
        {
            var sharedSteps = new SharedSteps();

            sharedSteps.LoggedAsUser();

            WebBrowser.Current.GoTo(UrlHelper.AbsoluteUrl("/moje-pytania"));

            sharedSteps.WhenIClickButton("Pytanie testowe z ekspertem");

            sharedSteps.WhenIClickButton("Odpowiedz");

            WhenITypeQuestionText("Kolejne testowe pytanie");

            sharedSteps.WhenIClickButton("Dodaj odpowiedź");

            AuthenticationHelper.LogoutUser();
        }

        [Given(@"że użytkownik zadał pytanie")]
        public void UserAskedQuestion()
        {
            var sharedSteps = new SharedSteps();

            sharedSteps.LoggedAsUser();

            WebBrowser.Current.GoTo(UrlHelper.AbsoluteUrl("/"));

            WhenIChooseCategoryAndTypeQuestionText("Dom", "Pytanie testowe");

            sharedSteps.WhenIClickButton("Zadaj pytanie ekspertowi!");

            sharedSteps.WhenIClickButton("Pomiń ten krok");

            new PaymentsSteps().WhenIFillPaymentForm();

            sharedSteps.WhenIClickButton("Uzyskaj odpowiedź");
        }
    }
}

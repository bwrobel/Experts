using Experts.Specs.Helpers;
using NUnit.Framework;
using TechTalk.SpecFlow;
using WatiN.Core;

namespace Experts.Specs.Steps
{
    [Binding]
    public class ProfileEditSteps
    {
        [When("kliknę Zapisz pod formularzem zmiany hasła")]
        public void WhenIClickSavePasswordButton()
        {
            ClickSubmitButton("/zmien-haslo");
        }

        [When("kliknę Zapisz pod formularzem zmiany adresu e-mail")]
        public void WhenIClickSaveEmailButton()
        {
            ClickSubmitButton("/zmien-email");
        }

        [When("kliknę Zapisz pod formularzem edycji mikroprofilu")]
        public void WhenIClickSaveMicroprofileButton()
        {
            ClickSubmitButton("/edytuj-mikroprofil");
        }

        [When("kliknę Zapisz pod formularzem edycji publicznej nazwy")]
        public void WhenIClickSavePublicNameButton()
        {
            ClickSubmitButton("/zmien-publiczna-nazwe-eksperta");
        }

        [When("prześlę zdjęcie użytkownika (.*)")]
        public void WhenIUploadAvatar(string filename)
        {
            var upload = WebBrowser.Current.FileUpload(Find.ById("avatarupload"));
            upload.Set(DataHelper.GetPath(filename));

            WebBrowser.Current.WaitForComplete();
        }

        [When("zaznaczę kategorię (.*)")]
        public void WhenIChooseCategory(string category)
        {
            var span = WebBrowser.Current.Span(Find.BySelector(".category-select span").And(Find.ByText(category)));
            span.Click();
        }

        [Then("zobaczę niestandardowe zdjęcie użytkownika")]
        public void ThenISeeCustomAvatar()
        {
            var image = WebBrowser.Current.Image(Find.BySelector("img[alt='user_avatar']"));
            
            StringAssert.DoesNotContain("defaultUserAvatar.jpg", image.Src);
        }

        private static void ClickSubmitButton(string action)
        {
            var selector = string.Format("form[action='{0}'] input[type='submit']", action);
            var button = WebBrowser.Current.Element(Find.BySelector(selector));
            button.Click();
        }
    }
}

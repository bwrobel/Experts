using WatiN.Core;

namespace Experts.Specs.Helpers
{
    public static class AuthenticationHelper
    {
        public static void LogInUser(string login, string password)
        {
            LogoutUser();

            WebBrowser.Current.GoTo(UrlHelper.AbsoluteUrl("/zaloguj-sie"));

            WebBrowser.Current.TextField(Find.ById("Login")).Value = login;
            WebBrowser.Current.TextField(Find.ById("Password")).Value = password;

            var signInButton = WebBrowser.Current.Button(Find.ByValue(Resources.Account.SignIn));        
            signInButton.Click();
        }

        public static void LogoutUser()
        {
            WebBrowser.Current.GoTo(UrlHelper.AbsoluteUrl("/" + Resources.Routes.Account_SignOut));
        }
    }
}

using System.Web;
using System.Web.Security;
using Experts.Core.Entities;
using Experts.Core.Entities.Events;
using Experts.Core.Utils;

namespace Experts.Web.Helpers
{
    public static class AuthenticationHelper
    {
        private static string AfterLoginUserEmail
        {
            get { return HttpContext.Current.Items["afterLoginUserEmail"] as string; }
            set { HttpContext.Current.Items["afterLoginUserEmail"] = value; }
        }

        private const string CurrentUserKey = "CurrentUser";

        private static string CurrentUserEmail
        {
            get 
            { 
                if(IsAuthenticated)
                    return AfterLoginUserEmail ?? HttpContext.Current.User.Identity.Name;

                return null;
            }
        }

        public static void Authenticate(string userEmail)
        {
            FormsAuthentication.SetAuthCookie(userEmail, true);
            AfterLoginUserEmail = userEmail;
        }

        public static User CurrentUser
        {
            get
            {
                if (!IsAuthenticated)
                    return null;

                if (!HttpContext.Current.Items.Contains(CurrentUserKey))
                    HttpContext.Current.Items.Add(CurrentUserKey, RepositoryHelper.Repository.User.FindByEmail(CurrentUserEmail));
                else if (HttpContext.Current.Items[CurrentUserKey] == null)
                    HttpContext.Current.Items[CurrentUserKey] = RepositoryHelper.Repository.User.FindByEmail(CurrentUserEmail);

                return (User)HttpContext.Current.Items[CurrentUserKey];
            }
        }

        public static bool IsExpert
        {
            get { return CurrentUser != null && CurrentUser.IsExpert; }
        }

        public static bool IsModerator
        {
            get { return CurrentUser != null && CurrentUser.IsModerator; }
        }

        public static bool IsAuthenticated
        {
            get { return HttpContext.Current != null  && (HttpContext.Current.Request.IsAuthenticated || AfterLoginUserEmail != null); }
        }

        public static bool IsNoSignUpUser
        {
            get { return CurrentUser != null && CurrentUser.IsNoSignUpUser; }
        }

        public static bool IsPartner
        {
            get { return CurrentUser != null && CurrentUser.IsPartner; }
        }

        public static bool IsPartnerRequestPending
        {
            get { return RepositoryHelper.Repository.Event.HasPendingEvent<BecomePartnerRequestEvent>(CurrentUser); }
        }

        public static bool HasActiveChats()
        {
            return IsAuthenticated && CurrentUser.HasActiveChats();
        }
    }

    public enum AuthenticationType
    {
        SignIn,
        SignUp,
        NoSignUp
    }
}
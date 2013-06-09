using System.Web.Mvc;
using Experts.Web.Helpers;

namespace Experts.Web.Filters
{
    public class AuthorizeSignedUpUserAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (AuthenticationHelper.IsAuthenticated && AuthenticationHelper.CurrentUser.IsNoSignUpUser)
                filterContext.Result = new HttpUnauthorizedResult();
            else
                base.OnAuthorization(filterContext);
        }
    }
}
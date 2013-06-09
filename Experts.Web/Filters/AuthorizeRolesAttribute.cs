using System;
using System.Web.Mvc;
using Experts.Web.Helpers;

namespace Experts.Web.Filters
{
    public class AuthorizeRolesAttribute : AuthorizeAttribute
    {
        public new Role? Roles { get; private set; } 

        public AuthorizeRolesAttribute(Role roles){Roles = roles;}

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            
            var userIsInRole = false;
            
            if((Roles & Role.Expert) == Role.Expert) 
                userIsInRole |= AuthenticationHelper.IsExpert;

            if((Roles & Role.Moderator) == Role.Moderator)
                userIsInRole |= AuthenticationHelper.IsModerator;

            if ((Roles & Role.Partner) == Role.Partner)
                userIsInRole |= AuthenticationHelper.IsPartner;

            if (!userIsInRole) 
                filterContext.Result = new HttpUnauthorizedResult();
        } 
    }

    [Flags]
    public enum Role
    {
        Expert = 1,
        Moderator = 2,
        Partner = 4
    }
}
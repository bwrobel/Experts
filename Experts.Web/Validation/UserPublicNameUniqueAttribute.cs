using System.ComponentModel.DataAnnotations;
using Experts.Core.Repositories;
using Experts.Web.Helpers;

namespace Experts.Web.Validation
{
    public class UserPublicNameUniqueAttribute : ValidationAttribute 
    {
        public UserPublicNameUniqueAttribute()
        {
            ErrorMessageResourceName = Resources.AccountConstants.DuplicatePublicName;
            ErrorMessageResourceType = typeof(Resources.Account);
        }

        public override bool IsValid(object value)
        {
            var name = value as string;
            var oldName = AuthenticationHelper.IsExpert
                              ? AuthenticationHelper.CurrentUser.Expert.PublicName
                              : AuthenticationHelper.CurrentUser.Moderator.PublicName;

            if (name == oldName)
                return true;

            using (var rf = new RepositoryFactory())
                return rf.User.IsPublicNameUnique(value as string);
        }
    }
}

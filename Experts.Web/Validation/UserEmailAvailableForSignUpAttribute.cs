using System.ComponentModel.DataAnnotations;
using Experts.Core.Repositories;

namespace Experts.Web.Validation
{
    public class UserEmailAvailableForSignUpAttribute : ValidationAttribute 
    {
        public UserEmailAvailableForSignUpAttribute()
        {
            ErrorMessageResourceName = Resources.AccountConstants.DuplicateEmail;
            ErrorMessageResourceType = typeof(Resources.Account);
        }

        public override bool IsValid(object value)
        {
            var email = value as string;
            using (var rf = new RepositoryFactory())
                return rf.User.IsEmailUnique(email) || rf.User.IsNoSignUpUser(email);
        }
    }
}

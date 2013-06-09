using System.ComponentModel.DataAnnotations;
using System.Web;
using Experts.Web.Helpers;
using Experts.Core.Utils;

namespace Experts.Web.Validation
{
    public class EmailUniqueIfAnonymous : ValidationAttribute
    {
        public EmailUniqueIfAnonymous()
        {
            ErrorMessageResourceName = Resources.AccountConstants.DuplicateEmail;
            ErrorMessageResourceType = typeof (Resources.Account);
        }

        public override bool IsValid(object value)
        {
            var email = (string) value;

            return AuthenticationHelper.IsAuthenticated || HttpContext.Current.GetDbRepository().User.IsEmailUnique(email);
        }
    }
}
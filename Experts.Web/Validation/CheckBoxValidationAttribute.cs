using System.ComponentModel.DataAnnotations;
using Experts.Web.Helpers;

namespace Experts.Web.Validation
{
    public class CheckBoxValidationAttribute : ValidationAttribute
    {
        public CheckBoxValidationAttribute()
        {
            ErrorMessageResourceName = Resources.AccountConstants.CheckBoxUnchecked;
            ErrorMessageResourceType = typeof(Resources.Account);
        }

        public override bool IsValid(object value)
        {
            if(AuthenticationHelper.IsAuthenticated)
            {
                return true;
            }

            return (bool)value;
        }
    }
}
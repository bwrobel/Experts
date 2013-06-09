using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Experts.Web.Validation
{
    public class UserPublicNameProperCharactersAttribute : ValidationAttribute 
    {
        public UserPublicNameProperCharactersAttribute()
        {
            ErrorMessageResourceName = Resources.AccountConstants.PublicNameWrongFormat;
            ErrorMessageResourceType = typeof(Resources.Account);
        }

        public override bool IsValid(object value)
        {
            var name = value as string;
            bool matched = Regex.IsMatch(name, "\u0022");
            if (matched) {return false;}
            return true;
        }
    }
}

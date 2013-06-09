using System.Text.RegularExpressions;

namespace Experts.Web.Validation
{
    public class EmailValidationAttribute : CheckBoxValidationAttribute
    {
        public EmailValidationAttribute()
        {
            ErrorMessageResourceName = Resources.FormsConstants.EmailIncorrect;
            ErrorMessageResourceType = typeof(Resources.Forms);
        }

        public override bool IsValid(object value)
        {
            var email = value as string;
            if (string.IsNullOrEmpty(email))
                return true;

            const string pattern = @"^[-!#$%&'*+/0-9=?A-Z^_a-z{|}~](\.?[-!#$%&'*+/0-9=?A-Z^_a-z{|}~])*@[a-zA-Z](-?[a-zA-Z0-9])*(\.[a-zA-Z](-?[a-zA-Z0-9])*)+$";
            var rx = new Regex(pattern);

            return rx.IsMatch(email);
        }
    }
}
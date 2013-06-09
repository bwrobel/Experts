namespace Experts.Web.Models.Forms
{
    public class QuickSignInForm
    {
        public SignInForm Form { get; set; }

        public string ErrorMessage { get; set; }

        public bool ShowSignUpOrResetPasswordMessage { get; set; }

        public bool ShowResendActivationMailInfo { get; set; }
    }
}
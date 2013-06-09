using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Experts.Core.Entities;
using Experts.Core.Utils;
using Experts.Web.Models.Payments;

namespace Experts.Web.Helpers
{
    public static class NoSignUpUserHelper
    {
        public static User CreateUser(PaymentForm form, UrlHelper url)
        {
            var repository = HttpContext.Current.GetDbRepository();

            var user = new User
                           {
                               Email = form.PersonalData.Email
                           };

            if (form.SignUp)
            {
                user.Password = form.PasswordForm.Password;
            }
            else
            {
                user.IsNoSignUpUser = true;
            }

            repository.User.Add(user);
            repository.User.AddEmailConfigurationDefaultValue(user);

            if (form.SignUp)
            {
                Flash.Info(Resources.Account.AccountCreatedFromPayment);
                Email.Send<ActivationEmail>(user);
            }
            else
            {
                Flash.Info(Resources.Account.AnonymousAccountCreated);
                Email.Send<ActivationEmail>(user);
            }

            AuthenticationHelper.Authenticate(user.Email);

            return user;
        }
    }
}
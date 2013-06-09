using System.Web.Mvc;
using System.Web.Security;
using Experts.Core.Entities;
using Experts.Web.Filters;
using Experts.Web.Helpers;
using Experts.Web.Models.Payments;

namespace Experts.Web.Controllers
{
    /* TODO: 
     *  - tytuł płatności (?)
    */

    public partial class PaymentController : BaseController
    {
        [DefaultRouting]
        public virtual ActionResult PaymentModal(int strategyId, int relatedId, decimal? value = null)
        {
            var model = new PaymentModel
                {
                    StrategyId = strategyId,
                    Value = value,
                    RelatedId = relatedId,
                };

            return PartialView(model);
        }

        [HttpPost]
        [DefaultRouting]
        public virtual ActionResult PaymentModal(PaymentForm paymentForm)
        {
            if (!ModelState.IsValid)
            {
                var model = new PaymentFormModel
                    {
                        PaymentForm = paymentForm,
                        Provider = PaymentHelper.PaymentProvider
                    };
                return PartialView(MVC.Payment.Views._PaymentForm, model);
            }

            var redirectModel = paymentForm.PreparePayment(Url);
            return PartialView(MVC.Payment.Views.PaymentRedirect, redirectModel);
        }

        [DefaultRouting]
        public virtual ActionResult PaymentForm(PaymentModel paymentModel, PaymentForm form = null)
        {
            if (form == null)
            {
                form = new PaymentForm {StrategyId = paymentModel.StrategyId};

                if (paymentModel.RelatedId.HasValue)
                    form.RelatedId = paymentModel.RelatedId.Value;

                if (AuthenticationHelper.IsAuthenticated)
                    form.ClonePayersDataFromPreviousTransfer(AuthenticationHelper.CurrentUser);
            }

            var model = new PaymentFormModel
                {
                    PaymentForm = form,
                    Provider = PaymentHelper.PaymentProvider
                };

            if (paymentModel.Value.HasValue)
            {
                model.HasEnoughFunds = PaymentHelper.HasEnoughFunds(paymentModel.Value.Value);
                model.Funds = PaymentHelper.GetFunds();
            }

            return PartialView(MVC.Payment.Views._PaymentForm, model);
        }

        [Authorize]
        public virtual ActionResult ProcessPayment()
        {
            if (!PaymentHelper.PaymentProvider.AuthenticateResult(Request))
                return HttpNotFound();

            var result = PaymentHelper.PaymentProvider.ParseResult(Request.Params);

            var payment = Repository.Payment.Get(result.PaymentId);
            payment.Status = result.IsSuccessfull ? PaymentStatus.Success : PaymentStatus.Failed;
            payment.ProviderPaymentId = result.ProviderPaymentId;
            Repository.Payment.Update(payment);

            var strategy = payment.GetStrategy(Url);
            strategy.AfterPayment();
            return result.ResponseResult ?? new EmptyResult();
        }

        [Authorize]
        public virtual ActionResult PaymentConfirmation(int paymentId, PaymentStatus providerStatus)
        {
            var payment = Repository.Payment.Get(paymentId);
            if (payment.User != AuthenticationHelper.CurrentUser)
                FormsAuthentication.RedirectToLoginPage();

            var strategy = payment.GetStrategy(Url);

            switch (payment.Status)
            {
                case PaymentStatus.Pending:
                    Flash.Info(Resources.Payment.PaymentPendingConfirmation);
                    break;
                case PaymentStatus.Success:
                    Flash.Success(Resources.Payment.PaymentSuccessConfirmation);
                    break;
                case PaymentStatus.Failed:
                    Flash.Error(Resources.Payment.PaymentFailureConfirmation);
                    break;
            }

            return RedirectToAction(strategy.PostPaymentAction);
        }
    }
}

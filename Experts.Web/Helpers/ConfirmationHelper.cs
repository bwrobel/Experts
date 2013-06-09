using System.Web.Mvc;
using Experts.Web.Models.Shared;
using System.Web.Mvc.Html;

namespace Experts.Web.Helpers
{
    public static class ConfirmationHelper
    {
        public static MvcHtmlString ActionLinkWithConfirmation(this HtmlHelper htmlHelper, string linkText, ActionResult result, string confirmationText, string tooltip = null, string icon = null, bool buttonLook = true)
        {
            var actionResult = (IT4MVCActionResult) result;
            var modalId = string.Format("{0}Modal", actionResult.Action);

            var model = new ActionLinkWithConfirmationModel
                            {
                                LinkText = linkText,
                                ConfirmationText = confirmationText,
                                ActionResult = result,
                                ModalId = modalId,
                                Tooltip = tooltip,
                                Icon = icon,
                                ButtonLook = buttonLook
                            };

            return htmlHelper.Partial(MVC.Shared.Views.ActionLinkWithConfirmation, model);
        }
    }
}
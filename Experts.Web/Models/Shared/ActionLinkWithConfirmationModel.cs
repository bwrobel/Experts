using System.Web.Mvc;

namespace Experts.Web.Models.Shared
{
    public class ActionLinkWithConfirmationModel
    {
        public string LinkText { get; set; }

        public string ConfirmationText { get; set; }

        public ActionResult ActionResult { get; set; }

        public string ModalId { get; set; }

        public string Tooltip { get; set; }

        public string Icon { get; set; }

        public bool ButtonLook { get; set; }
    }
}
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Experts.Web.Helpers
{
    public static class Flash
    {
        private const string KeyFlash = "Flash";

        private static FlashMessage SessionFlash
        {
            get { return HttpContext.Current.Session[KeyFlash] as FlashMessage; }
            set { HttpContext.Current.Session[KeyFlash] = value; }
        }

        private static void SetFlashMessage(string content, FlashMessageType type, bool link, int? data)
        {
            SessionFlash = new FlashMessage
                               {
                                   Content = content,
                                   Type = type,
                                   Link = link,
                                   Data = data
                               };
        }

        public static void Success(string message, bool link = false, int? data = null)
        {
            SetFlashMessage(message, FlashMessageType.Success, link, data);
        }

        public static void Error(string message, bool link = false, int? data = null)
        {
            SetFlashMessage(message, FlashMessageType.Error, link, data);
        }

        public static void Warning(string message, bool link = false, int? data = null)
        {
            SetFlashMessage(message, FlashMessageType.Warning, link, data);
        }

        public static void Info(string message, bool link = false, int? data = null)
        {
            SetFlashMessage(message, FlashMessageType.Info, link, data);
        }

        public static MvcHtmlString FlashMessage(this HtmlHelper htmlHelper)
        {
            var flashMessage = SessionFlash;
            if (flashMessage == null)
                return null;

            SessionFlash = null;

            return htmlHelper.Partial(MVC.Shared.Views.FlashMessage, flashMessage);
        }
    }

    public class FlashMessage
    {
        public string Content { get; set; }
        public FlashMessageType Type { get; set; }

        public bool Link { get; set; }
        public int? Data { get; set; }
    }

    public enum FlashMessageType
    {
        Success,
        Error,
        Warning,
        Info
    }
}
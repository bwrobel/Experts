using System;
using System.IO;
using System.Web.Mvc;
using Experts.Core.Entities;
using RazorEngine;

namespace Experts.Web.Helpers
{
    public static class RazorHelper
    {
        public static string RenderMailTemplate(EmailData model)
        {
            var templatePath = string.Format("{0}/Views/Emails/{1}.cshtml", AppDomain.CurrentDomain.BaseDirectory, model.TemplateName);
            var template = File.ReadAllText(templatePath);
            return Razor.Parse(template, model);
        }

        public static MvcHtmlString HtmlRaw(string text)
        {
            return MvcHtmlString.Create(text);
        }

        public static string EmailHeader(User user)
        {
            var header = "";
            if (user.IsExpert)
            {
                header = string.Format(Resources.Email.HeadExpert, user.Expert.PublicName);
            }
            if (!user.IsExpert)
            {
                header = Resources.Email.Head;
            }

            return header;
        }
    }
}

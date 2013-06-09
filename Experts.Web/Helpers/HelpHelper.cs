using System;
using System.Text.RegularExpressions;

namespace Experts.Web.Helpers
{
    public static class HelpHelper
    {
        public static string ChangeUrl(string text)
        {
            var regexText = text.ToLower()
                .Replace(" ", "-")
                .Replace("ć", "c")
                .Replace("ą","a")
                .Replace("ę","e")
                .Replace("ó","o")
                .Replace("ł", "l")
                .Replace("ś", "s")
                .Replace("ż", "z")
                .Replace("ź", "z");
            return Regex.Replace(regexText, "[^a-z-]+", "", RegexOptions.Compiled);
        }
    }
}
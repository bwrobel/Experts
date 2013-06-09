using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Experts.Core.Utils;
using System.Web.Routing;
using System.Collections.Specialized;

namespace Experts.Web.Helpers
{
    public static class StringExtensions
    {
        private const int MaxUrlPartSize = 80;
        private static readonly Dictionary<string, string> CustomReplacements = new Dictionary<string, string>
                                                                                  {
                                                                                      {"ł", "l"}
                                                                                  };

        public static string ToUrlPart(this string value)
        {
            var truncated = value.TruncateToWholeWord(MaxUrlPartSize);
            return Regex.Replace(truncated.ToASCII(), "[^a-zA-Z0-9]+", "-").ToLower().Trim('-');
        }

        public static string ShortenString(this string value, int limit)
        {
            return value.TruncateToWholeWord(limit, true);
        }

        public static string CutString(this string value, int limit, bool addDots = true)
        {
            int count = value.Count();

            if(count > limit && addDots)
            {
                return value.Remove(limit) + "...";
            }
            else
            {
                return value;
            }
        }

        public static string ToASCII(this string value)
        {
            var result = value;
            foreach (var customReplacement in CustomReplacements)
                result = result.Replace(customReplacement.Key, customReplacement.Value);

            var normalized = result.Normalize(NormalizationForm.FormD);
            var builder = new StringBuilder();
            foreach (var c in normalized)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                    builder.Append(c);
            }

            return builder.ToString();
        }

        public static RouteValueDictionary Append(this RouteValueDictionary dictionary, NameValueCollection values)
        {
            foreach (var key in values.AllKeys)
                dictionary[key] = values[key];

            return dictionary;
        }

        public static RouteValueDictionary Append(this RouteValueDictionary dictionary, string key, object value)
        {
            dictionary[key] = value;
            return dictionary;
        }
    }
}
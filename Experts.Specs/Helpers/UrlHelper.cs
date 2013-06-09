using System;
using System.Configuration;

namespace Experts.Specs.Helpers
{
    public static class UrlHelper
    {
        public static Uri AbsoluteUrl(string relativeUrl)
        {
            var baseUri = new Uri(BaseUrl);
            return new Uri(baseUri, relativeUrl);
        }

        public static string BaseUrl
        {
            get { return ConfigurationManager.AppSettings["baseUrl"]; }
        }
    }
}

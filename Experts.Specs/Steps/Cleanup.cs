using System;
using System.Configuration;
using System.IO;
using Experts.Specs.Helpers;
using TechTalk.SpecFlow;
using System.Net;

namespace Experts.Specs.Steps
{
    [Binding]
    public static class Cleanup
    {
        [BeforeScenario]
        public static void LogoutUser()
        {
            AuthenticationHelper.LogoutUser();
            WebBrowser.Current.GoTo(UrlHelper.AbsoluteUrl("/Development/RefreshDataFresh"));

            var mailDirectory = new DirectoryInfo(ConfigurationManager.AppSettings["mailDirectory"]);
            foreach(FileInfo file in mailDirectory.GetFiles())
            {
                file.Delete();
            }
        }

        [AfterScenario]
        public static void LogLastError()
        {
            if(ScenarioContext.Current.TestError != null)
            {
                var client = new WebClient();
                var lastError = client.DownloadString(UrlHelper.AbsoluteUrl("/Development/LastError"));

                if (!string.IsNullOrEmpty(lastError))
                {  
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine("Last system error:");
                    Console.WriteLine();
                    Console.WriteLine(lastError);
                    Console.WriteLine();
                }
            }
        }
    }
}

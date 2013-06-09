using System.Threading;
using TechTalk.SpecFlow;
using WatiN.Core;

namespace Experts.Specs.Helpers
{
    [Binding]
    public class WebBrowser
    {
        public static Browser Current { get; set; }
    }

    public static class BrowserExtensions
    {
        public static void WaitForAjaxRequest(this Browser browser)
        {
            int timeWaitedInMilliseconds = 0;
            var maxWaitTimeInMilliseconds = Settings.WaitForCompleteTimeOut*1000;

            while (browser.IsAjaxRequestInProgress()
                   && timeWaitedInMilliseconds < maxWaitTimeInMilliseconds)
            {
                Thread.Sleep(Settings.SleepTime);
                timeWaitedInMilliseconds += Settings.SleepTime;
            }
        }

        public static void InjectAjaxMonitor(this Browser browser)
        {
            const string monitorScript =
                @"function AjaxMonitor(){"
                + "var ajaxRequestCount = 0;"

                + "$(document).ajaxSend(function(){"
                + "    ajaxRequestCount++;"
                + "});"

                + "$(document).ajaxComplete(function(){"
                + "    ajaxRequestCount--;"
                + "});"

                + "this.isRequestInProgress = function(){"
                + "    return (ajaxRequestCount > 0);"
                + "};"
                + "}"

                + "var watinAjaxMonitor = new AjaxMonitor();";

            browser.Eval(monitorScript);
        }

        private static bool IsAjaxRequestInProgress(this Browser browser)
        {
            var evalResult = browser.Eval("watinAjaxMonitor.isRequestInProgress()");
            return evalResult == "true";
        }
    }
}

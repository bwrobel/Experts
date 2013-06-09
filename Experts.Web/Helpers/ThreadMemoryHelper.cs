using System.Web;
using Experts.Web.Models.Forms;

namespace Experts.Web.Helpers
{
    public static class ThreadMemoryHelper
    {
        private const string KeyRememberedThread = "KeyRememberedThread";

        private static ThreadForm RememberedThread
        {
            get { return HttpContext.Current.Session[KeyRememberedThread] as ThreadForm; }
            set { HttpContext.Current.Session[KeyRememberedThread] = value; }
        }

        public static void RememberThread(ThreadForm threadForm)
        {
            RememberedThread = threadForm;
        }

        public static ThreadForm PopRememberedThread()
        {
            var result = RememberedThread;
            RememberedThread = null;
            return result;
        }

        public static bool IsThreadRemembered()
        {
            return RememberedThread != null;
        }
    }
}
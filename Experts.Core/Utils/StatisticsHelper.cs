using System;

namespace Experts.Core.Utils
{
    public static class StatisticsHelper
    {
        public static int GetLastMonthNumber()
        {
            return (DateTime.Now.Month + 10) % 12 + 1;
        }
    }
}

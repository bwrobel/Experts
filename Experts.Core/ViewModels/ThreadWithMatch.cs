using Experts.Core.Entities;

namespace Experts.Core.ViewModels
{
    public class ThreadWithMatch
    {
        public Thread Thread { get; set; }
        public double Match { get; set; }

        public string MatchPercent
        {
            get
            {
                int percent = (int) (Match*100);
                return string.Format("{0}%", percent);
            }
        }
    }
}

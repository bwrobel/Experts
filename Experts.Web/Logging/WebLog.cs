namespace Experts.Web.Logging
{
    public class WebLog
    {
        public string SessionId { get; set; }

        public string Source { get; set; }

        public string Message { get; set; }

        public string Url { get; set; }

        public override string ToString()
        {
            return string.Format("Url:{0}|Source:{1}|SessionId:{2}|Message:{3}", Url, Source, SessionId, Message);
        }
    }
}

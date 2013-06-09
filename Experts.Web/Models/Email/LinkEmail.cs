namespace Experts.Web.Models.Email
{
    public class LinkEmail : ContactEmailBase
    {
        public string Subject { get; set; }
        public string Message { get; set; }
        public string Link { get; set; }
                
        public LinkEmail(string to)
            : base(/*MVC.Emails.Views.LinkEmail*/"LinkEmail.cshtml", to)
        {
        }
    }
}
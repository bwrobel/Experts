using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Experts.Core.Entities;

namespace Experts.Web.Models.Forms
{
    public class ReportIssueForm
    {
        [HiddenInput(DisplayValue = false)]
        public int ThreadId { get; set; }

        [Display(Name = Resources.ThreadConstants.ThreadReportIssueType, ResourceType = typeof(Resources.Thread))]
        public ThreadIssueType IssueType { get; set; }

        [Display(Name = Resources.ThreadConstants.ThreadReportIssueComment, ResourceType = typeof(Resources.Thread))]
        //[Required(ErrorMessageResourceName = Resources.ThreadConstants.ThreadReportIssueCommentRequired, ErrorMessageResourceType = typeof(Resources.Thread))]
        [DataType(DataType.MultilineText)]
        public string Comment { get; set; }
    }
}
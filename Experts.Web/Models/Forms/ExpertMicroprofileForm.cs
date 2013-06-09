using System.ComponentModel.DataAnnotations;

namespace Experts.Web.Models.Forms
{
    public class ExpertMicroprofileForm
    {
        [Display(Name = Resources.AccountConstants.MicroprofilePosition, ResourceType = typeof(Resources.Account))]
        [StringLength(25, ErrorMessageResourceName = Resources.AccountConstants.DescriptionTooLong, ErrorMessageResourceType = typeof(Resources.Account))]
        public string Position { get; set; }

        [Display(Name = Resources.AccountConstants.MicroprofileDescription, ResourceType = typeof(Resources.Account))]
        [DataType(DataType.MultilineText)]
        [StringLength(230, ErrorMessageResourceName = Resources.AccountConstants.DescriptionTooLong, ErrorMessageResourceType = typeof(Resources.Account))]
        public string Description { get; set; }
    }
}
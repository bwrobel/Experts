using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Experts.Web.Models.Forms
{
    public class PriceProposalForm
    {
        [HiddenInput(DisplayValue = false)]
        public int ThreadId { get; set; }

        [Display(Name = Resources.ThreadConstants.PriceProposalValue, ResourceType = typeof(Resources.Thread))]
        [Required(ErrorMessageResourceName = Resources.ThreadConstants.PriceProposalValueRequired, ErrorMessageResourceType = typeof(Resources.Thread))]
        public decimal? SurchargeValue { get; set; }

        [Display(Name = Resources.ThreadConstants.PriceProposalComment, ResourceType = typeof(Resources.Thread))]
        [Required(ErrorMessageResourceName = Resources.ThreadConstants.PriceProposalCommentRequired, ErrorMessageResourceType = typeof(Resources.Thread))]
        [DataType(DataType.MultilineText)]
        public string Comment { get; set; }
    }
}
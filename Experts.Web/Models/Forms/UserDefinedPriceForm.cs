using System.ComponentModel.DataAnnotations;

namespace Experts.Web.Models.Forms
{
    public class UserDefinedPriceForm
    {
        [Display(Name = Resources.ThreadConstants.PriceProposalValue, ResourceType = typeof(Resources.Thread))]
        [Required(ErrorMessageResourceName = Resources.ThreadConstants.UserDefinedPriceMustBeHigher, ErrorMessageResourceType = typeof(Resources.Thread))]
        public decimal? UserDefinedPrice { get; set; }
    }
}

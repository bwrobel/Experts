using System.ComponentModel.DataAnnotations;
using Experts.Core.Entities;

namespace Experts.Web.Models.Forms
{
    public class OpinionForm
    {
        [Required]
        [Display(Name = Resources.AccountConstants.OpinionContent, ResourceType = typeof(Resources.Account))]
        public string OpinionContent { get; set; }

        [Required]
        [Display(Name = Resources.AccountConstants.OpinionMark, ResourceType = typeof(Resources.Account))]
        public OpinionMark OpinionMark { get; set; }

        [Display(Name = Resources.AccountConstants.OpinionAddress, ResourceType = typeof(Resources.Account))]
        public string AddressCity { get; set; }
    }
}
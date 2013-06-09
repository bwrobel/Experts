using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Experts.Core.Entities;
using Experts.Web.Models.Shared;
using Experts.Web.Validation;

namespace Experts.Web.Models.Account
{
    public class ExpertCategoryAttributesModel
    {
        [Display(Name = Resources.AccountConstants.ExpertReasonTitle, ResourceType = typeof(Resources.Account))]
        [DataType(DataType.MultilineText)]
        [StringLength(200, ErrorMessageResourceName = Resources.AccountConstants.ExpertReasonTooLong, ErrorMessageResourceType = typeof(Resources.Account))]
        [AutoFocus]
        public string ExpertReason { get; set; }

        public Category Category { get; set; }
        public IEnumerable<CategoryAttribute> CategoryAttributes { get; set; }
        public AttributeValueModel[] CategoryAttributeValues { get; set; }
    }
}
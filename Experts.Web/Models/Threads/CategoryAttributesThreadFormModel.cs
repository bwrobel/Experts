using System.Collections.Generic;
using Experts.Core.Entities;
using Experts.Web.Models.Forms;
using Experts.Web.Models.Shared;

namespace Experts.Web.Models.Threads
{
    public class CategoryAttributesThreadFormModel
    {
        public ThreadForm ThreadForm { get; set; }
        public IEnumerable<CategoryAttribute> CategoryAttributes { get; set; }
        public AttributeValueModel[] CategoryAttributeValues { get; set; }
    }
}
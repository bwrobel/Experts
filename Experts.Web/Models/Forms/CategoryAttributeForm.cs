using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Experts.Core.Entities;

namespace Experts.Web.Models.Forms
{
    public class CategoryAttributeForm
    {
        public CategoryAttributeForm()
        {
            SelectedParentOptions = new List<SelectedParentOption>();
        }

        [ScaffoldColumn(false)]
        public int AttributeId { get; set; }

        [ScaffoldColumn(false)]
        public int? ParentAttributeId { get; set; }

        [ScaffoldColumn(false)]
        public int CategoryId { get; set; }

        [Display(Name = Resources.AdministrationConstants.CategoryAttributeName, ResourceType = typeof(Resources.Administration))]
        [Required(ErrorMessageResourceName = Resources.AdministrationConstants.CategoryAttributeNameRequired, ErrorMessageResourceType = typeof(Resources.Administration))]
        public string Name { get; set; }

        [Display(Name = Resources.AdministrationConstants.CategoryAttributeImportance, ResourceType = typeof(Resources.Administration))]
        [Required(ErrorMessageResourceName = Resources.AdministrationConstants.CategoryAttributeImportanceRequired, ErrorMessageResourceType = typeof(Resources.Administration))]
        public int Importance { get; set; }

        [Display(Name = Resources.AdministrationConstants.CategoryAttributeType, ResourceType = typeof(Resources.Administration))]
        public CategoryAttributeType Type { get; set; }
        
        [Display(Name = Resources.AdministrationConstants.CategoryAttributeOptions, ResourceType = typeof(Resources.Administration))]
        public IEnumerable<CategoryAttributeOption> Options { get; set; }

        public IEnumerable<CategoryAttributeOption> AvailableParentOptions { get; set; }

        [ScaffoldColumn(false)]
        public IEnumerable<SelectedParentOption> SelectedParentOptions { get; set; }

        public class SelectedParentOption
        {
            public int Id { get; set; }
            public bool IsSelected { get; set; }
        }
    }
}
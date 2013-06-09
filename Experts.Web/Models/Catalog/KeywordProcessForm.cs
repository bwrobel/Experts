using System.ComponentModel.DataAnnotations;
using Experts.Core.Entities;
using Experts.Web.Models.Shared;
using Experts.Web.Validation;

namespace Experts.Web.Models.Catalog
{
    public class KeywordProcessForm
    {
        public int SeoKeywordId { get; set; }

        [Required(ErrorMessageResourceName = Resources.CatalogConstants.KeyPhraseRequired, ErrorMessageResourceType = typeof(Resources.Catalog))]
        [IsKeywordUnique(ErrorMessageResourceName = Resources.CatalogConstants.KeyPhraseMustBeUnique, ErrorMessageResourceType = typeof(Resources.Catalog))]
        [Display(Name = Resources.CatalogConstants.SEOKeywordPhrase, ResourceType = typeof(Resources.Catalog))]
        public string SeoKeywordPhrase { get; set; }

        [Display(Name = Resources.CatalogConstants.SEOKeywordType, ResourceType = typeof(Resources.Catalog))]
        public SEOKeywordType SeoKeywordType { get; set; }

        [Display(Name = Resources.CatalogConstants.SeoKeywordCategory, ResourceType = typeof(Resources.Catalog))]
        public int CategoryId { get; set; }

        public AttributeValueModel[] AttributeValues { get; set; }
    }
}
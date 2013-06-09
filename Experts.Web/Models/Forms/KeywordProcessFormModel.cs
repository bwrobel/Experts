using System.Collections.Generic;
using System.Web.Mvc;
using Experts.Core.Entities;
using Experts.Web.Models.Catalog;
using Experts.Web.Models.Shared;

namespace Experts.Web.Models.Forms
{
    public class KeywordProcessFormModel
    {
        public KeywordProcessFormModel()
        {   
        }

        public KeywordProcessFormModel(SEOKeyword keyword)
        {
            SeoKeyword = keyword;
            KeywordProcessForm = new KeywordProcessForm { SeoKeywordId = keyword.Id, SeoKeywordPhrase = keyword.Phrase };
        }

        public int NumberOfKeywordsToModerate { get; set; }
        public KeywordProcessForm KeywordProcessForm { get; set; }
        public SEOKeyword SeoKeyword { get; private set; }
        public IEnumerable<CategoryAttribute> CategoryAttributes { get; set; }
        public AttributeValueModel[] CategoryAttributeValues { get; set; }
        public IEnumerable<SelectListItem> AvailableCategories { get; set; }
    }
}
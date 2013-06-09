using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Experts.Core.Entities;
using Experts.Core.Utils;
using Experts.Web.Models.Payments;
using Experts.Web.Models.Shared;

namespace Experts.Web.Models.Forms
{
    public class ThreadForm
    {
        [Required(ErrorMessageResourceName = Resources.ThreadConstants.ContentRequired, ErrorMessageResourceType = typeof(Resources.Thread))]
        public string Content { get; set; }

        [Required(ErrorMessageResourceName = Resources.ThreadConstants.CategoryNotSelected, ErrorMessageResourceType = typeof(Resources.Thread))]
        public int? CategoryId { get; set; }

        [Display(Name = Resources.ThreadConstants.ThreadPriority, ResourceType = typeof(Resources.Thread))]
        public ThreadPriority Priority { get; set; }

        [Display(Name = Resources.ThreadConstants.ThreadVerbosity, ResourceType = typeof(Resources.Thread))]
        public ThreadVerbosity Verbosity { get; set; }

        [Display(Name = Resources.ThreadConstants.ThreadExpertValue, ResourceType = typeof(Resources.Thread))]
        public decimal Value { get; set; }

        public AttributeValueModel[] AttributeValues { get; set; }
        
        public int? DirectQuestionExpertId { get; set; }
        public int? SeoKeywordId { get; set; }

        public int InterestedExpert { get; set; }

        public decimal? CustomValue { get; set; }

        public string TemporaryAttachmentFolder { get; set; }

        public PaymentForm PaymentForm { get; set; }

        public void GenerateTemporaryAttachmentFolder()
        {
            TemporaryAttachmentFolder = Generate();
        }

        private string Generate()
        {
            var key = CryptoHelper.CreateSalt();
            var regexKey = Regex.Replace(key, @"\W", string.Empty);
            return "temp" + regexKey;
        }
    }
}
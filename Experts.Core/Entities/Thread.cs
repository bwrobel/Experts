using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Experts.Core.Repositories;
using Experts.Core.Utils;

namespace Experts.Core.Entities
{
    public class Thread : IAuditableEntity, IService
    {
        public Thread()
        {
            Issues = new Collection<ThreadIssue>();
            CategoryAttributes = new Collection<CategoryAttributeValue>();
            Transfers = new Collection<Transfer>();
            PriceProposals = new Collection<PriceProposal>();
            State = ThreadState.Unoccupied;
        }

        public int Id { get; set; }

        public virtual User Author { get; set; }

        public virtual Expert Expert { get; set; }

        public virtual User Broker { get; set; }

        public virtual decimal BrokerProvision { get; set; }

        public virtual decimal ExpertProvision { get; set; }

        public virtual decimal AdditionalExpertProvisionValue { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<AdditionalService> AdditionalServices { get; set; }

        public IEnumerable<AdditionalService> VerifiedAdditionalServices { get { return AdditionalServices.Where(s => s.IsVerified); } }

        public virtual ICollection<Post> Posts { get; set; }

        public string Name 
        {
            get { return Posts != null && Posts.Count > 0 ? Posts.First().Content.TruncateToWholeWord(60,true) : string.Empty; }
        }

        [Display(Name = Resources.AdministrationConstants.CatalogThreadTitle, ResourceType = typeof(Resources.Administration))]
        [StringLength(60, ErrorMessageResourceName = Resources.AdministrationConstants.CatalogSanitizedThreadTitleTooLong, ErrorMessageResourceType = typeof(Resources.Administration))]
        public virtual string CatalogSanitizedThreadTitle { get; set; }

        public string CatalogThreadTitle
        {
            get { return String.IsNullOrWhiteSpace(CatalogSanitizedThreadTitle) ? Posts.First().GetPublicNameIfNotEmpty().TruncateToWholeWord(60) : CatalogSanitizedThreadTitle; }
        }

        public string GetSummary(int length, bool isPublicContentViable = false)
        {
            if (isPublicContentViable)
            {
                return Posts.First().GetPublicNameIfNotEmpty().TruncateToWholeWord(length, true);
            }

            return Posts.First().Content.TruncateToWholeWord(length, true);
        }

        public DateTime CreationDate { get; set; }

        public DateTime LastModificationDate { get; set; }

        public ThreadPriority Priority
        {
            get { return (ThreadPriority)IntPriority; }
            set { IntPriority = (int)value; }
        }

        public ThreadVerbosity Verbosity
        {
            get { return (ThreadVerbosity)IntVerbosity; }
            set { IntVerbosity = (int)value; }
        }

        public ThreadSanitizationStatus SanitizationStatus
        {
            get { return (ThreadSanitizationStatus) IntSanitizationStatus; }
            set { IntSanitizationStatus = (int) value; }
        }

        [Display(Name = Resources.ThreadConstants.ThreadState, ResourceType = typeof(Resources.Thread))]
        public ThreadState State
        {
            get { return (ThreadState)IntState; }
            set { IntState = (int)value; }
        }

        [Required]
        public int IntPriority { get; set; }

        [Required]
        public int IntVerbosity { get; set; }

        [Required]
        public int IntState { get; set; }

        [Required]
        public int IntSanitizationStatus { get; set; }

        public decimal Value { get; set; }

        public virtual Feedback Feedback { get; set; }

        public virtual ICollection<ThreadIssue> Issues { get; private set; }

        public DateTime? ExpertReleaseDate { get; set; }

        public DateTime? ThreadAcceptanceDate { get; set; }

        public DateTime? ThreadCloseDate
        {
            get
            {
                if (ThreadAcceptanceDate == null)
                    return null;

                return ThreadAcceptanceDate.Value.AddDays(ThreadRepository.ThreadAutoCloseIntervalInDays);
            }
        }

        public bool IsNotified { get; set; }

        public bool IsExpertResponseNotified { get; set; }

        public virtual ICollection<PriceProposal> PriceProposals { get; private set; }

        public PriceProposal AcceptedPriceProposal { get { return PriceProposals.FirstOrDefault(p => p.Accepted); } }

        public virtual ICollection<CategoryAttributeValue> CategoryAttributes { get; private set; } 

        public string PaymentType{get{return typeof (Thread).FullName;}}

        public virtual ICollection<Transfer> Transfers { get; private set; }

        void IService.AttachOwner(User owner)
        {
            Author = owner;
            foreach(var post in Posts)
                if (post.Author == null)
                    post.Author = owner;
        }

        public decimal GetSurchargeValue(int surchargeId)
        {
            return PriceProposals.First(p => p.Id == surchargeId).SurchargeValue;
        }

        public bool IsPaid { get; set; }

        public bool IsInner { get; set; }
    }

    public enum ThreadSanitizationStatus
    {
        NotSanitized = 0,
        Sanitized = 1,
        NotForPublic = 2
    }


    public enum ThreadState
    {
        Unoccupied = 1,
        Occupied = 2,
        Closed = 3,
        Accepted = 4,
        Reserved = 5,
        Hidden = 6
    }

    public enum ThreadPriority
    {
        Low = 1,
        Medium = 2,
        High = 3
    }

    public enum ThreadVerbosity
    {
        Low = 1,
        Medium = 2,
        High = 3
    }
}

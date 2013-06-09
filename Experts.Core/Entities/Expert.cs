using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Resources;

namespace Experts.Core.Entities
{
    public class Expert : IAuditableEntity
    {
        public Expert()
        {
            Categories = new Collection<Category>();
            Microprofiles = new Collection<ExpertMicroprofile>();
            CategoryAttributes = new Collection<ExpertCategoryAttributeValues>();
        }

        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        public string PublicName { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<Category> Categories { get; private set; }

        public virtual ICollection<ExpertMicroprofile> Microprofiles { get; private set; }

        public bool HasPicture { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        [Required]
        public DateTime LastModificationDate { get; set; }

        public double PositiveFeedback { get; set; }

        public int AcceptedAnswers { get; set; }

        public ExpertVerificationStatus VerificationStatus
        {
            get { return (ExpertVerificationStatus)IntVerificationStatus; }
            set { IntVerificationStatus = (int)value; }
        }

        public bool IsVerified { get { return VerificationStatus == ExpertVerificationStatus.Verified; } }

        [Required]
        public int IntVerificationStatus { get; set; }

        public string VerificationDetails { get; set; }

        public virtual Recommendation Recommendation { get; set; }

        public virtual ICollection<Thread> Answers { get; set; }

        public virtual ICollection<ExpertCategoryAttributeValues> CategoryAttributes { get; set; }
        
        public bool IsInner { get; set; }

        public virtual Provision Provision { get; set; }

    }

    public enum ExpertVerificationStatus
    {
        New = 0,
        Verified = 1,
        Rejected = 2,
        ToReverify = 3
    }

}

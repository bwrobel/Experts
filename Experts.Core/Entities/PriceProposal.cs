using System;
using System.ComponentModel.DataAnnotations;

namespace Experts.Core.Entities
{
    public class PriceProposal : IAuditableEntity
    {
        public int Id { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime LastModificationDate { get; set; }

        public PriceProposalVerificationStatus VerificationStatus
        {
            get { return (PriceProposalVerificationStatus)IntVerificationStatus; }
            set { IntVerificationStatus = (int)value; }
        }

        public int IntVerificationStatus { get; set; }


        [Required]
        public decimal SurchargeValue { get; set; }

        [Required]
        public string Comment { get; set; }

        [Required]
        public virtual Expert Expert { get; set; }

        [Required]
        public virtual Thread Thread { get; set; }

        [Required]
        public bool Accepted { get; set; }
    }

    public enum PriceProposalVerificationStatus
    {
        Proposed = 0,
        Verified = 1,
        Changed = 2,
        Declined = 3
    }
}

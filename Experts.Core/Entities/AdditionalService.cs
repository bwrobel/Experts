using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Experts.Core.Entities
{
    public class AdditionalService : IAuditableEntity, IService
    {
        public int Id { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime LastModificationDate { get; set; }

        [Required]
        public decimal Value { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Comment { get; set; }

        public virtual Expert Expert { get; set; }

        public virtual Thread Thread { get; set; }

        public bool? IsAccepted { get; set; }

        public bool IsVerified { get; set; }

        //IService

        void IService.AttachOwner(User user)
        {
            Expert = user.Expert;
        }

        public bool IsPaid { get; set; }

        public string Name { get { return Title; } }

        public string PaymentType { get { return typeof(AdditionalService).FullName; } }

        public virtual ICollection<Transfer> Transfers { get; private set; }
    }
}

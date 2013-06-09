using System;
using System.ComponentModel.DataAnnotations;

namespace Experts.Core.Entities
{
    public class Payment : IAuditableEntity
    {
        public int Id { get; set; }
        public string ProviderPaymentId { get; set; }

        public DateTime CreationDate { get; set; }
        public DateTime LastModificationDate { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public decimal Amount { get; set; }

        public virtual User User { get; set; }
        
        public int RelatedId { get; set; }

        public int SurchargeId { get; set; }

        public int StrategyId { get; set; }

        public PaymentStatus Status
        {
            get { return (PaymentStatus)IntStatus; }
            set { IntStatus = (int)value; }
        }

        [Required]
        public int IntStatus { get; set; }


        public decimal GetUserAvailableBalance()
        {
            return User == null ? decimal.Zero : User.GetAvailableCash();
        }

    }


    public enum PaymentStatus
    {
        Pending = 0,
        Success = 1,
        Failed = 2
    }

    //public enum PaymentType
    //{
    //    ForAnswer = 0
    //}
}

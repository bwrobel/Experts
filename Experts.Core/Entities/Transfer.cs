using System;

namespace Experts.Core.Entities
{
    /// <summary>
    /// Klasa reprezentująca pojedyńczą operację pieniężną obserwowaną z punktu widzenia konta użytkownika
    /// </summary>
    public class Transfer : IEntity
    {
        public int Id { get; set; }

        public virtual User Owner { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime? TransferDate { get; set; }

        public string Title { get; set; }

        public decimal Value { get; set; }

        public virtual Payment Payment { get; set; }

        public bool IsPending { get; set; }

        public string Comment { get; set; }

        public static Transfer Pending(decimal value, string title = Resources.PaymentConstants.TransferPendingThreadPayment)
        {
            return new Transfer
            {
                OrderDate = DateTime.Now,
                TransferDate = DateTime.Now,
                Title = title,
                Payment = null,
                Value = value,
                IsPending = true
            };
        }

        public static Transfer Cash(decimal value, string title = Resources.PaymentConstants.TransferCashThreadPayment, Payment payment = null, bool isTransfered = true, string comment = null)
        {
            return new Transfer
            {
                OrderDate = DateTime.Now,
                TransferDate = isTransfered ? DateTime.Now : (DateTime?)null,
                Title = title,
                Payment = payment,
                Value = value,
                IsPending = false,
                Comment = comment
            };
        }
    }
}

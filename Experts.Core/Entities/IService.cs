using System.Collections.Generic;

namespace Experts.Core.Entities
{
    public interface IService
    {
        int Id { get; }

        string Name { get; }

        string PaymentType { get; }

        decimal Value { get; }

        ICollection<Transfer> Transfers { get; }

        void AttachOwner(User owner);

        bool IsPaid { get; set; }
    }

    
}

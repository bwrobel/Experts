using System;

namespace Experts.Core.Entities
{
    public interface IAuditableEntity : IEntity
    {
        DateTime CreationDate { get; set; }
        DateTime LastModificationDate { get; set; }
    }
}

using System;
using Experts.Core.Data;
using Experts.Core.Entities;

namespace Experts.Core.Repositories
{
    public class AuditableRepository<T> : EntityRepository<T>
        where T : class, IAuditableEntity
    {
        public override void Add(T entity)
        {
            entity.SetDatesToNow();
            base.Add(entity);
        }

        public override void Update(T entity)
        {
            entity.UpdateModificationDate();
            base.Update(entity);
        }

        public AuditableRepository(DataContext db)
            : base(db){}
    }

    internal static class AuditableExtensions
    {
        public static void SetDatesToNow(this IAuditableEntity entity)
        {
            entity.CreationDate = DateTime.Now;
            entity.LastModificationDate = DateTime.Now;
        }

        public static void UpdateModificationDate(this IAuditableEntity entity)
        {
            entity.LastModificationDate = DateTime.Now;
        }
    }
}
using System.Linq;
using Experts.Core.Data;
using Experts.Core.Entities;

namespace Experts.Core.Repositories
{
    public class ProvisionRepository : EntityRepository<Provision>
    {
        public ProvisionRepository(DataContext db)
            : base(db)
        { }
    }
}

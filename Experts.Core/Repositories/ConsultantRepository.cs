using Experts.Core.Data;
using Experts.Core.Entities;

namespace Experts.Core.Repositories
{
    public class ConsultantRepository : EntityRepository<Consultant>
    {
        public ConsultantRepository(DataContext db)
            : base(db)
        {
        }
    }
}

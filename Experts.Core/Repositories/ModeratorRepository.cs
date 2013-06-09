using Experts.Core.Data;
using Experts.Core.Entities;

namespace Experts.Core.Repositories
{
    public class ModeratorRepository : EntityRepository<Moderator>
    {
        public ModeratorRepository(DataContext db)
            : base(db)
        {
        }
    }
}

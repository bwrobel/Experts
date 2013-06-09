using Experts.Core.Data;
using Experts.Core.Entities;
using Experts.Core.Utils;
using Experts.Core.ViewModels;
using System.Linq;

namespace Experts.Core.Repositories
{
    public class PartnerRepository : EntityRepository<Partner>
    {
        public PartnerRepository(DataContext db)
            : base(db)
        {
        }

        public PartnerMonthlyStatistics GetPartnerMonthlyStatistics(Partner partner)
        {
            var paymentStatistics = new UserRepository(Db).GetPaymentStatistics(partner.User.Id);

            var x = new PartnerMonthlyStatistics
                        {
                            MonthlyIncome =
                                paymentStatistics.Single(ps => ps.Month == StatisticsHelper.GetLastMonthNumber()).Value,
                            Balance = partner.User.GetTotalCash()
                        };
            return x;
        }

        public void AssignProvisionToPartner(Partner partner, ProvisionPartner provisionPartner)
        {
            var provision = Db.Provisions.Where(p => p.IntProvisionPartner == (int)provisionPartner).SingleOrDefault();
            partner.Provision = provision;
        }

        public void Create(User user)
        {
            var partner = new Partner
            {
                Provision = ThreadValueHelper.GetExpertBrokerStandardProvision(),
                User = user
            };
            Add(partner);
        }
    }
}

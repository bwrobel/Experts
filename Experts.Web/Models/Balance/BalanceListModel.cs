using Experts.Core.Entities;
using Experts.Web.Models.Shared;

namespace Experts.Web.Models.Balance
{
    public class BalanceListModel
    {
        public SortableGridModel<Transfer> GridModel { get; set; }

        public decimal TotalBalance { get; set; }

        public decimal AvailableBalance { get; set; }
    }
}
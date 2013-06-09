using System.Collections.Generic;
using MvcContrib.UI.Grid;

namespace Experts.Web.Models.Shared
{
    public class SortableGridModel<T>
        where T : class
    {
        public IEnumerable<T> Data { get; set; }
        public GridSortOptions SortOptions { get; set; }
        public Pagination Pagination { get; set; }
    }
}
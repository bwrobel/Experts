using System.Collections.Generic;

namespace Experts.Web.Models.Feedback
{
    public class OpinionListModel
    {
        public IEnumerable<Core.Entities.Feedback> Opinions { get; set; }
    }
}
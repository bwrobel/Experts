using System.Collections.Generic;
using Experts.Core.Entities;

namespace Experts.Web.Models.Threads
{
    public class AdditionalServiceInformation
    {
        public IEnumerable<AdditionalService> AdditionalServices { get; set; }

        public Thread Thread { get; set; }
    }
}
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Experts.Core.Entities
{
    public class Provision : IEntity
    {
        public Provision()
        {
            Experts = new Collection<Expert>();
            Partners = new Collection<Partner>();
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public decimal ProvisionValue { get; set; }

        public virtual ICollection<Expert> Experts { get; private set; }
        public virtual ICollection<Partner> Partners { get; private set; }

        public ProvisionExpert ProvisionExpert
        {
            get { return (ProvisionExpert)IntProvisionExpert; }
            set { IntProvisionExpert = (int)value; }
        }
        public int IntProvisionExpert { get; set; }

        public ProvisionPartner ProvisionPartner
        {
            get { return (ProvisionPartner)IntProvisionPartner; }
            set { IntProvisionPartner = (int)value; }
        }
        public int IntProvisionPartner { get; set; }
    }

    public enum ProvisionPartner
    {
        StandardPartner = 1
    }

    public enum ProvisionExpert
    {
        Novice = 1,
        Intermediate = 2,
        Experienced = 3,
        FullProfit = 4,
    }
}

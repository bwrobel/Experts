namespace Experts.Core.Entities
{
    public class Partner : IEntity
    {
        public int Id { get; set; }

        public string Url { get; set; }

        public string BankAccount { get; set; }

        public string CompanyName { get; set; }

        public string VatNumber { get; set; }

        public string Address { get; set; }

        public virtual User User { get; set; }

        public virtual Provision Provision { get; set; }
    }
}

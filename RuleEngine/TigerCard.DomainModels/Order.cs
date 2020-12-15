using RuleEnginePlatform;

namespace TigerCard.DomainModels
{
    public class Order : IFact
    {
        public long Id { get; }

        public Customer Customer { get; set; }

        public bool IsOpen { get; set; }

        public bool IsDiscounted { get; private set; }

        public double PercentDiscount { get; private set; }

        public Order(long id, Customer customer)
        {
            Id = id;
            Customer = customer;
        }

        public void ApplyDiscount(double discount)
        {
            IsDiscounted = true;
            PercentDiscount = discount;
        }
    }
}
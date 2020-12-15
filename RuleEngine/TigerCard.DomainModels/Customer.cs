using RuleEnginePlatform;

namespace TigerCard.DomainModels
{
    public class Customer : IFact
    {
        public long Id { get; }

        public string Name { get; }

        public bool IsPreferred { get; }

        public Customer(long id, string name, bool isPreferred)
        {
            Id = id;
            Name = name;
            IsPreferred = isPreferred;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

namespace RuleEnginePlatform
{
    internal partial class Repository<TFact> : IRepository<TFact> where TFact : IFact
    {
        private readonly Dictionary<Int64, TFact> _underlyingCollection = new Dictionary<Int64, TFact>();

        public void Add(TFact fact)
        {
            _underlyingCollection[fact.Id] = fact;
            FactAdded?.Invoke(this, new FactAddedEventArgs(fact));
        }

        public void Update(TFact fact, string property)
        {
            _underlyingCollection[fact.Id] = fact;
            FactUpdated?.Invoke(this, new FactUpdatedEventArgs(fact, property));
        }

        public TFact Get(long id) => _underlyingCollection.TryGetValue(id, out var fact) ? fact : default;

        public IEnumerable<TFact> Get(Func<TFact, bool> predicate) => _underlyingCollection.Values.Where(predicate);
    }
}
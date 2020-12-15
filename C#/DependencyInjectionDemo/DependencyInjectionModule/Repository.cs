using System;
using System.Collections.Generic;

namespace DependencyInjectionModule
{
    internal class Repository<TFact> : IRepository<TFact> where TFact : Fact
    {
        private readonly Dictionary<Int64, TFact> _underlyingCollection = new Dictionary<Int64, TFact>();

        public void Add(TFact fact)
        {
            _underlyingCollection[fact.Id] = fact;
        }

        public TFact Get(long id) => _underlyingCollection.TryGetValue(id, out var fact) ? fact : default;
    }
}
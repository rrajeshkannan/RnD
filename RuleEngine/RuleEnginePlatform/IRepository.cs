using System;
using System.Collections.Generic;

namespace RuleEnginePlatform
{
    internal interface IRepository<TFact> where TFact : IFact
    {
        void Add(TFact fact);

        void Update(TFact fact, string property);

        public TFact Get(Int64 id);

        public IEnumerable<TFact> Get(Func<TFact, bool> predicate);

        IObservable<TFact> FactsAdded { get; }

        IObservable<(TFact Fact, string Property)> FactsUpdated { get; }
    }
}
using System;
using System.Collections.Generic;

namespace RuleEnginePlatform
{
    public interface IContext
    {
        void Add<TFact>(TFact fact) where TFact : IFact;

        void Update<TFact>(TFact fact, string property) where TFact : IFact;

        TFact Get<TFact>(Int64 id) where TFact : IFact;

        IEnumerable<TFact> Get<TFact>(Func<TFact, bool> predicate) where TFact : IFact;
    }
}
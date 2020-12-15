using System;
using System.Reactive.Linq;

namespace RuleEnginePlatform
{
    public partial class Context : IRuleDefinitionContext
    {
        public IObservable<(IContext Context, TFact Fact)> OnNew<TFact>() where TFact : IFact
        {
            return _factsRepository.GetFactRepository<TFact>()
                .FactsAdded
                .Select(fact => ((IContext)this, fact));
        }

        public IObservable<(IContext Context, TFact Fact, string Property)> OnChange<TFact>() where TFact : IFact
        {
            return _factsRepository.GetFactRepository<TFact>()
                .FactsUpdated
                .Select(fact => ((IContext)this, fact.Fact, fact.Property));
        }
    }
}
using System;

namespace RuleEnginePlatform
{
    public interface IRuleDefinitionContext
    {
        IObservable<(IContext Context, TFact Fact)> OnNew<TFact>() where TFact : IFact;

        IObservable<(IContext Context, TFact Fact, string Property)> OnChange<TFact>() where TFact : IFact;
    }
}
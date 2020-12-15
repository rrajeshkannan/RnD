using System;
using System.Linq.Expressions;

namespace RuleEnginePlatform
{
    public interface ILeftHandSideExpression
    {
        ILeftHandSideExpression Match<TInference>(params Expression<Func<TInference, bool>>[] conditions) 
            where TInference : IInference;

        ILeftHandSideExpression Exists<TFact>(params Expression<Func<TFact, bool>>[] conditions) 
            where TFact : IFact;

        ILeftHandSideExpression Query<TFact, TResult>(params Expression<Func<TFact, TResult>>[] queryExpressions) 
            where TFact : IFact
            where TResult : IFact;
    }
}
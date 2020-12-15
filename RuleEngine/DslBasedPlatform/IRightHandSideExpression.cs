using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace RuleEnginePlatform
{
    public interface IRightHandSideExpression
    {
        IRightHandSideExpression Do(Expression<Action> action);
    }
}
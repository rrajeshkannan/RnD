using System;
using System.Collections.Generic;

namespace RuleEnginePlatform
{
    public interface IRule
    {
        public string Name { get; }

        public void Compile(Context context);

        public ILeftHandSideExpression When();

        public IRightHandSideExpression Then();
    }
}
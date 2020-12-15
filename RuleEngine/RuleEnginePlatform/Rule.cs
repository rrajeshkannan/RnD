using System;

namespace RuleEnginePlatform
{
    public abstract class Rule : IRule
    {
        public abstract string Name { get; }

        public void Compile(IRuleDefinitionContext context)
        {
            Define(context);
        }

        public abstract void Define(IRuleDefinitionContext context);
    }
}
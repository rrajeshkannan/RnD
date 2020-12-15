using System;
using System.Collections.Generic;
using System.Text;

namespace RuleEnginePlatform
{
    public class Context
    {
        internal FactsRepository FactsRepository => new FactsRepository();
        internal InferencesRepository InferencesRepository => new InferencesRepository();

        internal Context()
        {
        }

        internal void Build(RuleRepository ruleRepository)
        {
            var rules = ruleRepository.Rules;

            foreach (var rule in rules)
            {
                rule.Compile(this);
            }
        }
    }
}
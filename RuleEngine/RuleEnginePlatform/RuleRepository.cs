using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace RuleEnginePlatform
{
    public class RuleRepository : IRuleRepository
    {
        private IEnumerable<IRule> _rules;

        public void LoadFrom(params Assembly[] assemblies)
        {
            var ruleTypes = assemblies.SelectMany(a => a.DefinedTypes.Where(IsRuleType));
            _rules = ruleTypes.Select(CreateRuleInstance);
        }

        public void Compile(IRuleDefinitionContext context)
        {
            foreach (var rule in _rules)
            {
                rule.Compile(context);
            }
        }

        private static bool IsRuleType(TypeInfo typeInfo)
        {
            return
                IsConcreteType() &&
                IsDerivedFromRuleType();

            bool IsConcreteType()
            {
                return !typeInfo.IsAbstract &&
                    !typeInfo.IsInterface &&
                    !typeInfo.IsGenericTypeDefinition;
            }

            bool IsDerivedFromRuleType()
            {
                var ruleType = typeof(Rule).GetTypeInfo();
                return ruleType.IsAssignableFrom(typeInfo);
            }
        }

        private static IRule CreateRuleInstance(TypeInfo ruleType) => (Rule)Activator.CreateInstance(ruleType);
    }
}
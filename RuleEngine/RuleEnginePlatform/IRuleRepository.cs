using System.Reflection;

namespace RuleEnginePlatform
{
    public interface IRuleRepository
    {
        void LoadFrom(params Assembly[] assemblies);

        void Compile(IRuleDefinitionContext context);
    }
}
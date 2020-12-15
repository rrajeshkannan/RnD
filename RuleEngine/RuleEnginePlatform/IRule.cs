namespace RuleEnginePlatform
{
    public interface IRule
    {
        string Name { get; }

        void Compile(IRuleDefinitionContext context);

        void Define(IRuleDefinitionContext context);
    }
}
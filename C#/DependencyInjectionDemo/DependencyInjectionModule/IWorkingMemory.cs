namespace DependencyInjectionModule
{
    public interface IWorkingMemory
    {
        IRepository<TFact> Repository<TFact>() where TFact : Fact;
    }
}
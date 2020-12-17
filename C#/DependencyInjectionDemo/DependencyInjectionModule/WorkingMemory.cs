using Autofac;

namespace DependencyInjectionModule
{
    public class WorkingMemory : IWorkingMemory
    {
        private readonly IComponentContext _container;

        public WorkingMemory(IComponentContext container)
            => _container = container;

        public IRepository<TFact> Repository<TFact>() where TFact : Fact
        {
            return _container.Resolve<IRepository<TFact>>();
        }

        //public void Add<TFact>(TFact fact) where TFact : Fact
        //{
        //    GetRepository<TFact>().Add(fact);
        //}
    }
}
using Autofac;
using CustomModule.Domain;
using DependencyInjectionModule;

namespace CustomModule
{
    public class Module : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            //builder.RegisterRepositoryForFactType<MyFact>();
            //builder.RegisterRepositoryForFactType<MyFact>();

            builder.RegisterFactsFromAssembly(typeof(MyFact).Assembly);
            builder.RegisterFactsFromAssembly(typeof(MyFact).Assembly);
        }
    }
}
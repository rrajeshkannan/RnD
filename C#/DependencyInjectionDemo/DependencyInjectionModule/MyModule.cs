using Autofac;

namespace DependencyInjectionModule
{
    public class MyModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<ConsoleOutputService>().As<IOutputService>();
            builder.RegisterType<TodayWriterService>().As<IDateWriterService>();
            
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
        }
    }
}
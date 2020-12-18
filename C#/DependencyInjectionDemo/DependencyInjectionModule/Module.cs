using Autofac;

namespace DependencyInjectionModule
{
    public class Module : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<ConsoleOutputService>().As<IOutputService>();
            builder.RegisterType<TodayWriterService>().As<IDateWriterService>();
            
            // TODO: this shall be removed?
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
            builder.RegisterType<WorkingMemory>().As(typeof(IWorkingMemory)).InstancePerLifetimeScope();

            builder.RegisterType<Session>().As<ISession>().InstancePerLifetimeScope();
        }
    }
}
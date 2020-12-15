using Autofac;
using DependencyInjectionModule;

namespace DependencyInjectionDemo
{
    public static class ContainerConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<MyModule>();

            builder.RegisterType<Application>().As<IApplication>();

            return builder.Build();
        }
    }
}
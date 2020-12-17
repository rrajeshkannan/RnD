using Autofac;
using System;

namespace DependencyInjectionDemo
{
    // https://autofaccn.readthedocs.io/en/latest/getting-started/
    // https://codereview.stackexchange.com/a/56207
    // https://gist.github.com/greatb/1bfd9a5bd579a65e4eee1c4b074dacd0
    class Program
    {
        private static IContainer Container { get; set; }

        static void Main()
        {
            Container = ContainerConfig.Configure();

            using (var scope = Container.BeginLifetimeScope())
            {
                // Resolve services
                var app = scope.Resolve<IApplication>();
                app.Run();
            }

            Console.ReadKey();
        }
    }
}
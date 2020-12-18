using Autofac;
using DependencyInjection.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DependencyInjectionModule
{
    public static class RuleAssemblyExtensions
    {
        //public static void RegisterRepositoryForFactType<TFact>(this ContainerBuilder builder) where TFact : Fact
        //{
        //    //builder.RegisterType<Repository<TFact>>()
        //    //    .AsSelf()
        //    //    .As<IRepository<TFact>>()
        //    //    .As<IRepository>()
        //    //    .IfNotRegistered(typeof(Repository<TFact>))
        //    //    .InstancePerLifetimeScope();

        //    builder.RegisterType(typeof(Repository<TFact>))
        //        .AsSelf()
        //        .As(typeof(IRepository<TFact>))
        //        .As(typeof(IRepository))
        //        .IfNotRegistered(typeof(Repository<TFact>))
        //        .InstancePerLifetimeScope();
        //}

        public static void RegisterFactsFromAssembly(this ContainerBuilder builder, Assembly assembly)
        {
            var factTypes = assembly.DefinedTypes
                .Where(IsFactType)
                .Select(factTypeInfo => factTypeInfo.AsType());

            foreach (var factType in factTypes)
            {
                Type concreteRepository = typeof(Repository<>).MakeGenericType(factType);
                Type interfaceRepository = typeof(IRepository<>).MakeGenericType(factType);

                builder.RegisterType(concreteRepository)
                    .AsSelf() // equivalent to -> .As(concreteRepository)
                    .As(interfaceRepository)
                    .As(typeof(IRepository))
                    .IfNotRegistered(concreteRepository)
                    .InstancePerLifetimeScope();
            }
        }

        private static bool IsFactType(TypeInfo typeInfo)
        {
            return
                typeInfo.IsConcreteType() &&
                typeInfo.IsDerivedFrom<Fact>();
        }
    }
}
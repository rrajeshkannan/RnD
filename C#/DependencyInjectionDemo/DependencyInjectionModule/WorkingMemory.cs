using Autofac;
using DependencyInjection.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DependencyInjectionModule
{
    public class WorkingMemory : IWorkingMemory
    {
        private readonly IComponentContext _container;

        private readonly IDictionary<Type, IRepository> _repositories;// = new Dictionary<Type, IRepository>();

        public WorkingMemory(
            IComponentContext container,
            IEnumerable<IRepository> repositories)
        {
            _container = container;

            _repositories = repositories.ToDictionary(
                repository => GetFactTypeFrom(repository.GetType()));
            
            //foreach (var repository in repositories)
            //{
            //    var factType = repository.GetType()
            //        .GetInterfaces()
            //        .Where(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IRepository<>))
            //        .Select(repositoryFactType => repositoryFactType.GetGenericArguments()[0])
            //        .First();

            //    _repositories.Add(factType, repository);
            //}
        }

        private static Type GetFactTypeFrom(Type repositoryType)
        {
            return repositoryType.GetInterfaces()
                .Where(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IRepository<>))
                .SelectMany(repositoryFactType => repositoryFactType.GetGenericArguments())
                .First();
        }

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
using Autofac;
using DependencyInjection.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DependencyInjectionModule
{
    public class WorkingMemory : IWorkingMemory
    {
        //private readonly IComponentContext _container;

        private readonly IDictionary<Type, IRepository> _repositories;// = new Dictionary<Type, IRepository>();

        public WorkingMemory(
            //IComponentContext container,
            IEnumerable<IRepository> repositories)
        {
            //_container = container;

            _repositories = repositories.ToDictionary(
                repository => GetFactTypeFrom(repository.GetType()));
        }

        private static Type GetFactTypeFrom(Type repositoryType)
        {
            return repositoryType.GetInterfaces()
                .Where(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IRepository<>))
                .SelectMany(repositoryFactType => repositoryFactType.GetGenericArguments())
                .First(factType => factType.IsConcreteType() && factType.IsDerivedFrom<Fact>());
        }

        public IRepository<TFact> Repository<TFact>() where TFact : Fact
        {
            if (!_repositories.TryGetValue(typeof(TFact), out IRepository repositoryGeneric))
            {
                throw new InvalidOperationException();
            }

            return repositoryGeneric as IRepository<TFact>;

            //return _container.Resolve<IRepository<TFact>>();
        }

        //public void Add<TFact>(TFact fact) where TFact : Fact
        //{
        //    GetRepository<TFact>().Add(fact);
        //}
    }
}
using System;
using System.Collections.Generic;

namespace RuleEnginePlatform
{
    public class FactsRepository
    {
        private readonly Dictionary<Type, object> _repository = new Dictionary<Type, object>();

        public IRepository<TFact> GetFactRepository<TFact>() where TFact : IFact
        {
            if (!_repository.TryGetValue(typeof(TFact), out var repository))
            {
                repository = new Repository<TFact>();
                _repository.Add(typeof(TFact), repository);
            }

            return (IRepository<TFact>)repository;
        }

        private readonly Dictionary<Type, IEnumerable<IFact>> _repository1 = new Dictionary<Type, IEnumerable<IFact>>();

        public IEnumerable<TFact> GetFactRepository1<TFact>() where TFact : IFact
        {
            if (!_repository1.TryGetValue(typeof(TFact), out var repository))
            {
                repository = new List<IFact>();
                _repository.Add(typeof(TFact), repository);
            }

            return (IEnumerable<TFact>)repository;
        }
    }
}
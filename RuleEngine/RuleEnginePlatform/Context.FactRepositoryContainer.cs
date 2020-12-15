using System;
using System.Collections.Generic;

namespace RuleEnginePlatform
{
    partial class Context
    {
        private class FactRepositoryContainer
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
        }
    }
}
using System;
using System.Collections.Generic;

namespace RuleEnginePlatform
{
    public interface IRepository<TEntity> where TEntity : IFact
    {
        public TEntity Get(Int64 key);

        public IEnumerable<TEntity> Get();
    }
}
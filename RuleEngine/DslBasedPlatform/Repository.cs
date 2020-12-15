using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace RuleEnginePlatform
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : IFact
    {
        //public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null)
        //{
        //    throw new NotImplementedException();
        //}

        public TEntity Get(long key)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> Get()
        {
            throw new NotImplementedException();
        }
    }
}
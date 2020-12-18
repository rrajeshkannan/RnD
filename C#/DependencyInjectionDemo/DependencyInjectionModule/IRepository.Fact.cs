using DependencyInjection.Domain;
using System;

namespace DependencyInjectionModule
{
    public interface IRepository<TFact> : IRepository where TFact : Fact
    {
        void Add(TFact fact);

        TFact Get(Int64 id);
    }
}
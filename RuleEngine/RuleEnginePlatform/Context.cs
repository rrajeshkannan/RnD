using System;
using System.Collections.Generic;

namespace RuleEnginePlatform
{
    public partial class Context : IContext
    {
        private FactRepositoryContainer _factsRepository => new FactRepositoryContainer();

        internal Context()
        {
        }

        internal void Build(IRuleRepository ruleRepository)
        {
            ruleRepository.Compile(this);
        }

        public void Add<TFact>(TFact fact) where TFact : IFact
        {
            if (fact == null)
            {
                throw new ArgumentNullException(nameof(fact));
            }

            _factsRepository.GetFactRepository<TFact>()
                .Add(fact);
        }

        public void Update<TFact>(TFact fact, string property) where TFact : IFact
        {
            if (fact == null)
            {
                throw new ArgumentNullException(nameof(fact));
            }

            _factsRepository.GetFactRepository<TFact>()
                .Update(fact, property);
        }

        public TFact Get<TFact>(Int64 id) where TFact : IFact
        {
            return _factsRepository.GetFactRepository<TFact>()
                .Get(id);
        }

        public IEnumerable<TFact> Get<TFact>(Func<TFact, bool> predicate) where TFact : IFact
        {
            return _factsRepository.GetFactRepository<TFact>()
                .Get(predicate);
        }
    }
}
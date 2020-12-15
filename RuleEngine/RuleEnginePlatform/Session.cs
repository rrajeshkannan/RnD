using System;

namespace RuleEnginePlatform
{
    public class Session
    {
        private Context _context;

        public Session()
        {
        }

        public void Build(IRuleRepository ruleRepository)
        {
            if (ruleRepository == null)
            {
                throw new ArgumentNullException(nameof(ruleRepository));
            }

            _context = new Context();

            _context.Build(ruleRepository);
        }

        public void Add<TFact>(TFact fact) where TFact : IFact
        {
            if (_context == null)
            {
                throw new InvalidOperationException($"Session should be initialized with method {nameof(Build)} before adding facts");
            }

            _context.Add(fact);
        }
    }
}
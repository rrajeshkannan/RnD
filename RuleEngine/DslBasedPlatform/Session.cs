using System;
using System.Collections.Generic;
using System.Text;

namespace RuleEnginePlatform
{
    public class Session
    {
        private Context _context;

        public Session ()
        {
        }

        public void Build(RuleRepository ruleRepository)
        {
            _context = new Context();

            _context.Build(ruleRepository);
        }
    }
}
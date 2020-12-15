using System;

namespace DependencyInjectionModule
{
    public class Fact2 : Fact
    {
        public override long Id { get; }

        public Fact2()
        {
            Id = 20;
        }
    }
}

using System;

namespace DependencyInjectionModule
{
    public class MyFact : Fact
    {
        public override Int64 Id { get; }

        public MyFact(Int64 id)
            => Id = id;
    }
}
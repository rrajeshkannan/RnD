namespace DependencyInjectionModule
{
    public class Fact1 : Fact
    {
        public override long Id { get; }

        public Fact1()
        {
            Id = 10;
        }
    }
}
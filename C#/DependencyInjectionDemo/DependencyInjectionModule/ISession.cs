namespace DependencyInjectionModule
{
    public interface ISession
    {
        void Add<TFact>(TFact fact) where TFact : Fact;

        void WriteDate();
    }
}
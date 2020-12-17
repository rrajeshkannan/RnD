using DependencyInjectionModule;

namespace DependencyInjectionDemo
{
    public class Application : IApplication
    {
        private readonly ISession _session;

        public Application(ISession session)
            => _session = session;

        public void Run()
        {
            _session.WriteDate();
            _session.Add(new MyFact(1));
        }
    }
}
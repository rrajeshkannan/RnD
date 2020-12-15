using DependencyInjectionModule;

namespace DependencyInjectionDemo
{
    public class Application : IApplication
    {
        private readonly IDateWriterService _dateWriter;

        public Application(IDateWriterService dateWriter)
        {
            _dateWriter = dateWriter;
        }

        public void Run()
        {
            _dateWriter.WriteDate();
        }
    }
}
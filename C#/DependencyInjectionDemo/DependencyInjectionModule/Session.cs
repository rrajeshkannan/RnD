namespace DependencyInjectionModule
{
    internal class Session : ISession
    {
        private readonly IWorkingMemory _workingMemory;

        private readonly IDateWriterService _dateWriter;

        public Session(
            IWorkingMemory workingMemory,
            IDateWriterService dateWriter)
        {
            _workingMemory = workingMemory;
            _dateWriter = dateWriter;
        }

        public void Add<TFact>(TFact fact) where TFact : Fact
        {
            var repository = _workingMemory.Repository<TFact>();

            repository.Add(fact);
        }

        public void WriteDate()
        {
            _dateWriter.WriteDate();
        }
    }
}
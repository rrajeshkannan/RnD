using System;

namespace DependencyInjectionModule
{
    // This TodayWriter is where it all comes together.
    // Notice it takes a constructor parameter of type IOutput 
    //   - that lets the writer write to anywhere based on the implementation. 
    // Further, it implements WriteDate such that today's date is written out;
    //   - you could have one that writes in a different format or a different date.
    public class TodayWriterService : IDateWriterService
    {
        private IOutputService _output;

        public TodayWriterService(IOutputService output)
        {
            _output = output;
        }

        public void WriteDate()
        {
            _output.Write(DateTime.Today.ToShortDateString());
        }
    }
}
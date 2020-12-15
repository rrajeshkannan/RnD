using System;

namespace DependencyInjectionModule
{
    // This implementation of the IOutput interface is actually how we write to the Console. 
    // Technically we could also implement IOutput to write to Debug or Trace... or anywhere else.
    public class ConsoleOutputService : IOutputService
    {
        public void Write(string content)
        {
            Console.WriteLine(content);
        }
    }
}
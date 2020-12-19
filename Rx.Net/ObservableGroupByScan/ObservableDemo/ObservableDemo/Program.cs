using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

namespace ObservableDemo
{
    class Program
    {
        static void Main()
        {
            //IntegerRepository.Run();
            //DateRepository.Run();
            //CustomerFareRepository.SimpleRun();
            //CustomerFareRepository.MaxTotalRun();

            ChainedHandlersCheck.Run();

            Console.ReadKey();
        }
    }
}
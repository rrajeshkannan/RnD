using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

namespace ObservableDemo
{
    public class Repository
    {
        private class NumberAddedEventArgs : EventArgs
        {
            public int Number { get; }

            public NumberAddedEventArgs(int number) => Number = number;
        }

        private event EventHandler<NumberAddedEventArgs> NumberAdded;

        public IObservable<int> NumbersAdded { get; }

        public Repository()
        {
            var numbersAddedObservable = Observable.FromEventPattern<NumberAddedEventArgs>(
                handler => NumberAdded += handler,
                handler => NumberAdded -= handler);

            NumbersAdded = numbersAddedObservable
                .Select(args => args.EventArgs.Number)
                .Publish()
                .RefCount();
        }

        private IList<int> _underlyingCollection = new List<int>();

        public void Add(int number)
        {
            _underlyingCollection.Add(number);
            NumberAdded?.Invoke(this, new NumberAddedEventArgs(number));
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var repository = new Repository();

            var groupedNumbers = repository.NumbersAdded
                .GroupBy(number => number % 3)
                .Select(numberGroup => 
                    numberGroup.Scan(
                        (Group: numberGroup.Key, Numbers: new List<int>()),
                        (state, number) => { state.Numbers.Add(number); return (state.Group, state.Numbers); }))
                .Merge();

            var subscription = groupedNumbers
                .Subscribe(numberListGroup =>
                {
                    Console.Write($"Group: {numberListGroup.Group}, Numbers: ");
                    foreach (var number in numberListGroup.Numbers)
                    {
                        Console.Write($"{number}, ");
                    }
                    Console.WriteLine();
                });

            for (int i = 0; i < 15; i++)
            {
                repository.Add(i);
            }

            Console.ReadKey();
            subscription.Dispose();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

namespace ObservableDemo
{
    internal static class IntegerRepository
    {
        public static void Run()
        {
            var repository = new Repository<int>();

            var groupedNumbers = repository.ItemsAdded
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

            subscription.Dispose();
        }
    }
}
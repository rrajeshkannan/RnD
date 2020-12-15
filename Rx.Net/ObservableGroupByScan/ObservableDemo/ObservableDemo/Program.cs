using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

namespace ObservableDemo
{
    public class Repository<TItem>
    {
        private class ItemAddedEventArgs<T> : EventArgs
        {
            public T Item { get; }

            public ItemAddedEventArgs(T number) => Item = number;
        }

        private event EventHandler<ItemAddedEventArgs<TItem>> ItemAdded;

        public IObservable<TItem> ItemsAdded { get; }

        public Repository()
        {
            var itemsAddedObservable = Observable.FromEventPattern<ItemAddedEventArgs<TItem>>(
                handler => ItemAdded += handler,
                handler => ItemAdded -= handler);

            ItemsAdded = itemsAddedObservable
                .Select(args => args.EventArgs.Item)
                .Publish()
                .RefCount();
        }

        private IList<TItem> _underlyingCollection = new List<TItem>();

        public void Add(TItem item)
        {
            _underlyingCollection.Add(item);
            ItemAdded?.Invoke(this, new ItemAddedEventArgs<TItem>(item));
        }
    }

    class Program
    {
        static void Main()
        {
            // IntegerRepositoryDemo();
            DateRepositoryDemo();

            Console.ReadKey();
        }

        private static void IntegerRepositoryDemo()
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

        private static void DateRepositoryDemo()
        {
            var repository = new Repository<DateTime>();

            var groupedDateTimes = repository.ItemsAdded
                .GroupBy(date => date.Date)
                .Select(dateGroup =>
                    dateGroup.Scan(
                        (Group: dateGroup.Key, Dates: new List<DateTime>()),
                        (state, date) => { state.Dates.Add(date); return (state.Group, state.Dates); }))
                .Merge();

            var subscription = groupedDateTimes
                .Subscribe(dateListGroup =>
                {
                    Console.Write($"Group: {dateListGroup.Group}, Dates: ");
                    foreach (DateTime date in dateListGroup.Dates)
                    {
                        Console.Write($"<{date.Day}-{date.Month}-{date.Year} {date.Hour}:{date.Minute}:{date.Hour}>, ");
                    }
                    Console.WriteLine();
                });

            DateTime[] dates = new DateTime[]
            {
                new DateTime(2020, 12, 07),
                new DateTime(2020, 12, 08),
                new DateTime(2020, 12, 09),
            };

            for (int i = 0; i < 15; i++)
            {
                var date = dates[i % 3];
                var dateWithDifferentTime = new DateTime(date.Year, date.Month, date.Day, 10, i, 10);

                repository.Add(dateWithDifferentTime);
            }

            subscription.Dispose();
        }
    }
}
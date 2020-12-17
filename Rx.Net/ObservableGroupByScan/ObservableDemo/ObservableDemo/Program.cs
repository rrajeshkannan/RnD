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
            // DateRepositoryDemo();
            // CustomerFareRepositoryDemo();
            //CustomerFareRepositoryMaxTotalDemo();

            //DateTime date = DateTime.Today;
            //DateTime date = new DateTime(2020, 12, 31);
            DateTime date = new DateTime(2020, 01, 01);
            DateTime previousDate = date.AddDays(-1);
            Console.WriteLine(previousDate);

            DateTime nextDate = date.AddDays(1);
            Console.WriteLine(nextDate);

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
                var date = dates[i % dates.Length];
                var dateWithDifferentTime = new DateTime(date.Year, date.Month, date.Day, 10, i, 10);

                repository.Add(dateWithDifferentTime);
            }

            subscription.Dispose();
        }

        private class CustomerFare
        {
            public int Id { get; }

            public DateTime DateTime { get; }

            public decimal Fare { get; }

            public CustomerFare(int id, DateTime dateTime, decimal fare)
                => (Id, DateTime, Fare) = (id, dateTime, fare);
        }

        private static void CustomerFareRepositoryDemo()
        {
            var repository = new Repository<CustomerFare>();

            var groupedCustomerFares = repository.ItemsAdded
                .GroupBy(customerFare => (customerFare.Id, customerFare.DateTime.Date))
                .Select(customerFareGroup =>
                    customerFareGroup.Scan(
                        (Group: customerFareGroup.Key, CustomerFares: new List<CustomerFare>()),
                        (state, customerFare) =>
                        {
                            state.CustomerFares.Add(customerFare);
                            return (state.Group, state.CustomerFares);
                        }))
                .Merge();

            var subscription = groupedCustomerFares
                .Subscribe(customerFareGroup =>
                {
                    var group = customerFareGroup.Group;

                    var customerId = group.Id;
                    var groupDate = group.Date;

                    Console.WriteLine(
                        $"Group: <{customerId}::{groupDate.Day}-{groupDate.Month}-{groupDate.Year}>, Fares: ");
                    foreach (var customerFare in customerFareGroup.CustomerFares)
                    {
                        var date = customerFare.DateTime;
                        Console.Write($"\t<{customerFare.Id}::{date.Day}_{date.Month}_{date.Year} {date.Hour}_{date.Minute}_{date.Hour}::{customerFare.Fare}>, ");
                    }
                    Console.WriteLine();
                });

            DateTime[] dates = new DateTime[]
            {
                new DateTime(2020, 12, 07),
                new DateTime(2020, 12, 08),
                new DateTime(2020, 12, 09),
                new DateTime(2020, 12, 10)
            };

            int[] ids = new int[] { 1, 2 };

            for (int i = 0; i < 15; i++)
            {
                var date = dates[i % dates.Length];
                var dateWithDifferentTime = new DateTime(date.Year, date.Month, date.Day, 10, i, 10);

                var id = ids[i % ids.Length];

                var customerFare = new CustomerFare(id, dateWithDifferentTime, i * 100);

                repository.Add(customerFare);
            }

            subscription.Dispose();
        }

        private static void CustomerFareRepositoryMaxTotalDemo()
        {
            var repository = new Repository<CustomerFare>();

            var groupedCustomerFares = repository.ItemsAdded
                .GroupBy(customerFare => (customerFare.Id, customerFare.DateTime.Date))
                .Select(customerFareGroup =>
                    customerFareGroup.Scan(
                        (Group: customerFareGroup.Key, Total: 0m, Max: 0m, CustomerFare: (CustomerFare)null),
                        (state, customerFare) =>
                        {
                            return (state.Group, state.Total + customerFare.Fare, Math.Max(state.Max, customerFare.Fare), customerFare);
                        }))
                .Merge();

            var subscription = groupedCustomerFares
                .Subscribe(customerFareGroup =>
                {
                    var group = customerFareGroup.Group;

                    var customerId = group.Id;
                    var groupDate = group.Date;

                    var customerFare = customerFareGroup.CustomerFare;
                    var fareId = customerFare.Id;
                    var rideDate = customerFare.DateTime;
                    var fare = customerFare.Fare;

                    Console.Write($"Group: [{customerId}::{groupDate.Day}-{groupDate.Month}-{groupDate.Year}], ");
                    Console.Write($"Total: <{customerFareGroup.Total}>, Max: <{customerFareGroup.Max}>, ");
                    Console.WriteLine($"Item: [{fareId}]:<{rideDate.Day}-{rideDate.Month}-{rideDate.Year}>, Fare: {fare}");
                });

            DateTime[] dates = new DateTime[]
            {
                new DateTime(2020, 12, 07),
                new DateTime(2020, 12, 08),
                new DateTime(2020, 12, 09),
                new DateTime(2020, 12, 10)
            };

            int[] ids = new int[] { 1, 2 };

            for (int i = 0; i < 15; i++)
            {
                var date = dates[i % dates.Length];
                var dateWithDifferentTime = new DateTime(date.Year, date.Month, date.Day, 10, i, 10);

                var id = ids[i % ids.Length];

                var customerFare = new CustomerFare(id, dateWithDifferentTime, i * 100);

                repository.Add(customerFare);
            }

            subscription.Dispose();
        }
    }
}
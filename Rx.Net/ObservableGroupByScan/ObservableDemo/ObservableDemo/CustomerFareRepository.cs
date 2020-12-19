using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObservableDemo
{
    internal class CustomerFare
    {
        public int Id { get; }

        public DateTime DateTime { get; }

        public decimal Fare { get; }

        public CustomerFare(int id, DateTime dateTime, decimal fare)
            => (Id, DateTime, Fare) = (id, dateTime, fare);
    }

    internal static class CustomerFareRepository
    {
        public static void SimpleRun()
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

        public static void MaxTotalRun()
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
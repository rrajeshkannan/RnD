using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

namespace ObservableDemo
{
    public class DateRepository
    {
        public static void Run()
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
    }
}
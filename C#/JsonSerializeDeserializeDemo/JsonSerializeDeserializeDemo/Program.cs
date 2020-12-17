using JsonSerializeDeserializeDemo.Cap;
using JsonSerializeDeserializeDemo.TimeOfTravel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using Tiger.Card.FareCalculation.Domain.TariffChart;
using Tiger.MetroTransport.Domain.RouteMap;

namespace JsonSerializeDeserializeDemo
{
    public class Forecast
    {
        [JsonInclude]
        public DateTime Date { get; private set; }

        [JsonInclude]
        public int TemperatureC { get; private set; }

        [JsonInclude]
        public string Summary { private get; set; }

        public Forecast(DateTime date, int temperatureC, string summary)
        {
            Date = date;
            TemperatureC = temperatureC;
            Summary = summary;
        }
    };

    class Program
    {
        static void Main()
        {
            var options = new JsonSerializerOptions
            {
                //IgnoreReadOnlyProperties = false,
                WriteIndented = true
            };

            //string json = @"{""Date"":""2020-10-23T09:51:03.8702889-07:00"",""TemperatureC"":40,""Summary"":""Hot""}";
            //Console.WriteLine($"Input JSON: {json}");

            //Forecast forecast = JsonSerializer.Deserialize<Forecast>(json);
            //Console.WriteLine($"Date: {forecast.Date}");
            //Console.WriteLine($"TemperatureC: {forecast.TemperatureC}");

            //Forecast forecast = new Forecast(DateTime.Now, 40, "Hot");

            //var json = JsonSerializer.Serialize<Forecast>(forecast, new JsonSerializerOptions { WriteIndented = true });
            //File.WriteAllText("forecast.json", json);
            //Console.WriteLine($"Serialized JSON: {json}");

            //json = File.ReadAllText(@"forecast.json");
            //var forecastDeserialized = JsonSerializer.Deserialize<Forecast>(json);

            //return;

            var Zone1 = new Zone(1, "1");
            var Zone2 = new Zone(2, "2");
            //var intraZoneFare1 = new IntraZoneFare(1, Zone1, 30, 25);
            //var intraZoneFare2 = new IntraZoneFare(2, Zone2, 25, 20);
            //var interZoneFare1 = new InterZoneFare(3, Zone1, Zone2, 35, 30);
            //var interZoneFare2 = new InterZoneFare(4, Zone2, Zone1, 30, 35);

            //var intraZoneFares = new List<IntraZoneFare> { intraZoneFare1, intraZoneFare2 };
            //var interZoneFares = new List<InterZoneFare> { interZoneFare1, interZoneFare2 };

            //var jsonString = JsonSerializer.Serialize(intraZoneFares, options);
            //File.WriteAllText("intraZoneFares.json", jsonString);

            //jsonString = JsonSerializer.Serialize(interZoneFares, options);
            //File.WriteAllText("interZoneFares.json", jsonString);

            //jsonString = File.ReadAllText("intraZoneFares.json");
            //var intraZoneFaresRead = JsonSerializer.Deserialize<List<IntraZoneFare>>(jsonString, options);

            //jsonString = File.ReadAllText("interZoneFares.json");
            //var interZoneFaresRead = JsonSerializer.Deserialize<List<InterZoneFare>>(jsonString, options);


            // =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-


            //var jsonString = File.ReadAllText(@"TariffChart\Configs\TariffChart.IntraZoneFares.json");
            //var intraZoneFaresRead = JsonSerializer.Deserialize<IntraZoneFare[]>(jsonString, options);

            //jsonString = File.ReadAllText(@"TariffChart\Configs\TariffChart.InterZoneFares.json");
            //var interZoneFaresRead = JsonSerializer.Deserialize<InterZoneFare[]>(jsonString, options);

            // =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-

            //var weekdayPeakHours = new List<TimePeriod>
            //{
            //    new TimePeriod(1, (07, 00, 00), (10, 30, 00)),
            //    new TimePeriod(2, (17, 00, 00), (20, 00, 00))
            //};

            //var jsonString = JsonSerializer.Serialize(weekdayPeakHours, options);
            //File.WriteAllText("peakHours.weekday.json", jsonString);

            //var weekendPeakHours = new List<TimePeriod>
            //{
            //    new TimePeriod(1, (09, 00, 00), (11, 30, 00)),
            //    new TimePeriod(2, (18, 00, 00), (22, 00, 00))
            //};

            //jsonString = JsonSerializer.Serialize(weekendPeakHours, options);
            //File.WriteAllText("peakHours.weekend.json", jsonString);

            //var jsonString = File.ReadAllText("peakHours.weekday.json");
            //var weekdayPeakHoursRead = JsonSerializer.Deserialize<TimePeriod[]>(jsonString, options);

            //jsonString = File.ReadAllText("peakHours.weekend.json");
            //var weekendPeakHoursRead = JsonSerializer.Deserialize<TimePeriod[]>(jsonString, options);

            // =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-

            //var intraZoneCaps = new List<IntraZoneCap>
            //{
            //    new IntraZoneCap(1, CapKind.Daily, Zone1, 100m),
            //    new IntraZoneCap(2, CapKind.Daily, Zone2, 80m),
            //    new IntraZoneCap(1, CapKind.Weekly, Zone1, 500m),
            //    new IntraZoneCap(2, CapKind.Weekly, Zone2, 400m),
            //};

            //var jsonString = JsonSerializer.Serialize(intraZoneCaps, options);
            //File.WriteAllText("Caps.IntraZone.json", jsonString);

            //var interZoneCaps = new List<InterZoneCap>
            //{
            //    new InterZoneCap(1, CapKind.Daily, Zone1, Zone2, 120m),
            //    new InterZoneCap(1, CapKind.Daily, Zone2, Zone1, 120m),
            //    new InterZoneCap(1, CapKind.Weekly, Zone1, Zone2, 600m),
            //    new InterZoneCap(1, CapKind.Weekly, Zone2, Zone1, 600m),
            //};

            //jsonString = JsonSerializer.Serialize(interZoneCaps, options);
            //File.WriteAllText("Caps.InterZone.json", jsonString);

            var jsonString = File.ReadAllText("Caps.IntraZone.json");
            var intraZoneCapsRead = JsonSerializer.Deserialize<IntraZoneCap[]>(jsonString, options);

            jsonString = File.ReadAllText("Caps.InterZone.json");
            var interZoneCapsRead = JsonSerializer.Deserialize<InterZoneCap[]>(jsonString, options);

            //jsonString = File.ReadAllText("WeeklyCaps.IntraZone.json");
            //var intraZoneWeeklyCapsRead = JsonSerializer.Deserialize<IntraZoneWeeklyCap[]>(jsonString, options);

            //jsonString = File.ReadAllText("WeeklyCaps.InterZone.json");
            //var interZoneWeeklyCapsRead = JsonSerializer.Deserialize<InterZoneWeeklyCap[]>(jsonString, options);

            // =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-
        }
    }
}
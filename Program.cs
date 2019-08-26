using System;
using Newtonsoft.Json;

namespace Ex_DateTimeZoneHandling
{
    class Program
    {
        #region variables    
        private readonly static JsonSerializerSettings local = new JsonSerializerSettings
        {
            DateTimeZoneHandling = DateTimeZoneHandling.Local
        };
        private readonly static JsonSerializerSettings utc = new JsonSerializerSettings
        {
            DateTimeZoneHandling = DateTimeZoneHandling.Utc
        };
        private readonly static JsonSerializerSettings unspecified = new JsonSerializerSettings
        {
            DateTimeZoneHandling = DateTimeZoneHandling.Unspecified
        };
        private readonly static JsonSerializerSettings roundtripKind = new JsonSerializerSettings
        {
            DateTimeZoneHandling = DateTimeZoneHandling.RoundtripKind
        };
        #endregion

        static void Main(string[] args)
        {
            ExpDeserialize();
            ExpSerialize();
        }

        static void ExpDeserialize()
        {
            var json = @"{
                ""Date"": ""2019-12-31T15:00:00.000Z""
            }";

            Console.WriteLine("\nDeserialization result:");
            Console.WriteLine("Local:         " + Deserialize(json, local));
            Console.WriteLine("UTC:           " + Deserialize(json, utc));
            Console.WriteLine("Unspecified:   " + Deserialize(json, unspecified));
            Console.WriteLine("RoundtripKind: " + Deserialize(json, roundtripKind));
        }

        static MyModel Deserialize(string json, JsonSerializerSettings settings)
        {
            return JsonConvert.DeserializeObject<MyModel>(json, settings);
        }

        static void ExpSerialize()
        {
            // Copied from https://www.newtonsoft.com/json/help/html/SerializeDateTimeZoneHandling.htm
            Flight flight = new Flight
            {
                Date = new DateTime(2013, 1, 21, 0, 0, 0, DateTimeKind.Unspecified),
                DateUtc = new DateTime(2013, 1, 21, 0, 0, 0, DateTimeKind.Utc),
                DateLocal = new DateTime(2013, 1, 21, 0, 0, 0, DateTimeKind.Local),
            };

            string jsonWithRoundtripTimeZone = JsonConvert.SerializeObject(flight, Formatting.Indented, new JsonSerializerSettings
            {
                DateTimeZoneHandling = DateTimeZoneHandling.RoundtripKind
            });

            Console.WriteLine("\nRoundtripKind:");
            Console.WriteLine(jsonWithRoundtripTimeZone);

            string jsonWithLocalTimeZone = JsonConvert.SerializeObject(flight, Formatting.Indented, new JsonSerializerSettings
            {
                DateTimeZoneHandling = DateTimeZoneHandling.Local
            });

            Console.WriteLine("\nLocal:");
            Console.WriteLine(jsonWithLocalTimeZone);

            string jsonWithUtcTimeZone = JsonConvert.SerializeObject(flight, Formatting.Indented, new JsonSerializerSettings
            {
                DateTimeZoneHandling = DateTimeZoneHandling.Utc
            });

            Console.WriteLine("\nUtc:");
            Console.WriteLine(jsonWithUtcTimeZone);

            string jsonWithUnspecifiedTimeZone = JsonConvert.SerializeObject(flight, Formatting.Indented, new JsonSerializerSettings
            {
                DateTimeZoneHandling = DateTimeZoneHandling.Unspecified
            });

            Console.WriteLine("\nUnspecified:");
            Console.WriteLine(jsonWithUnspecifiedTimeZone);
        }
    }

    class MyModel
    {
        public DateTime Date { get; set; }

        public override string ToString()
        {
            return $"Date={Date} Date.Kind={Date.Kind}";
        }
    }

    class Flight
    {
        public DateTime Date { get; set; }
        public DateTime DateUtc { get; set; }
        public DateTime DateLocal { get; set; }
    }
}

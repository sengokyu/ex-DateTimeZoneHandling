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
        }

        static void ExpDeserialize()
        {
            var json = @"{
                ""Date"": ""2019-12-31T15:00:00.000Z"",
                ""DateWithOffset"": ""2019-12-31T15:00:00.000Z""
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
    }

    class MyModel
    {
        public DateTime Date { get; set; }
        public DateTimeOffset DateWithOffset { get; set; }

        public override string ToString()
        {
            return $"Date={Date} Date.Kind={Date.Kind} DateWithOffset={DateWithOffset}";
        }
    }
}

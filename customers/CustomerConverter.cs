using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace customers
{
    public class CustomerConverter : JsonConverter
    {
        public override bool CanWrite => false;

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Customer);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jo = JObject.Load(reader);
            var properties = jo.Properties().ToList();
            if (properties.Count != 2) return new Customer();

            var customer = new Customer
            {
                Id = properties[0].Value.ToString(),
                Name = properties[1].Value.ToString()
            };

            return customer;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
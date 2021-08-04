using System.Collections;
using Newtonsoft.Json;

namespace customers
{
    [JsonConverter(typeof(CustomerConverter))]
    public class Customer
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
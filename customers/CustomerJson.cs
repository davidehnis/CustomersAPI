using System.Collections.Generic;
using Newtonsoft.Json;

namespace customers
{
    public class CustomerJson
    {
        [JsonProperty("customers")]
        public List<Customer> Customers { get; set; }
    }
}
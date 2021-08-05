using System.Collections;
using Newtonsoft.Json;

namespace customers
{
    [JsonConverter(typeof(CustomerConverter))]
    public class Customer
    {
        public Customer()
        {
        }

        public Customer(int id, string name)
        {
            Id = id.ToString();
            Name = name;
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public override string ToString()
        {
            return $"{Id}:{Name}";
        }
    }
}
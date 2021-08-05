using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace customers
{
    public class CustomerDatabase
    {
        public static IEnumerable<Customer> Fetch()
        {
            try
            {
                var json = File.ReadAllText("database.json");
                var data = JsonConvert.DeserializeObject<Container>(json, new CustomerConverter());

                return data?.Customers;
            }
            catch (Exception)
            {
                return new List<Customer>();
            }
        }

        public static Customer Get(int customerId)
        {
            try
            {
                var json = File.ReadAllText("database.json");
                var data = JsonConvert.DeserializeObject<Container>(json, new CustomerConverter());

                return data?.Customers.FirstOrDefault(c => c.Id == customerId.ToString());
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static void Save(Customer customer)
        {
            var customers = Fetch().ToList();
            var exists = customers.FirstOrDefault(c => c.Id == customer.Id);
            var container = new Container();
            if (exists == null)
            {
                customers.Add(customer);
                container.Customers = customers.ToArray();
                Write(container);
                return;
            }

            exists.Name = customer.Name;
            container.Customers = customers.ToArray();

            Write(container);
        }

        private static void Write(Container container)
        {
            var contents = JsonConvert.SerializeObject(container, new CustomerConverter());
            File.WriteAllText("database.json", contents);
        }
    }
}
using IdentityModel.Client;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace customers.client
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            // Create identity client
            var client = new HttpClient();
            var discoveryDocument = await client.GetDiscoveryDocumentAsync("https://localhost:5001");
            if (discoveryDocument.IsError)
            {
                Console.WriteLine(discoveryDocument.Error);
                return;
            }

            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = discoveryDocument.TokenEndpoint,

                ClientId = "client",
                ClientSecret = "customers.api.secret",
                Scope = "customers.api"
            });

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }

            Console.WriteLine(tokenResponse.Json);
            Console.WriteLine("");
            var customersApiClient = new CustomersApiClient();

            // Fetching all Customers
            var fetched = await customersApiClient.Fetch(tokenResponse.AccessToken);
            var customers = fetched.ToList();
            if (customers.Any()) Console.WriteLine("Fetched Customers");
            foreach (var c in customers)
            {
                Console.WriteLine($"Customer => {c}");
            }
            Console.WriteLine("");

            // Getting a customer by Id
            var customer = await customersApiClient.Get(tokenResponse.AccessToken, 1);
            if (customer != null)
            {
                Console.WriteLine($"Retrieved Customer {customer}");

                // Updating a customer
                customer.Name = "Robert";
                await customersApiClient.Save(tokenResponse.AccessToken, customer);
            }
            Console.WriteLine("");

            // Adding a customer
            var newCustomer = new Customer(899, "Fox");
            await customersApiClient.Save(tokenResponse.AccessToken, newCustomer);

            var final = await customersApiClient.Fetch(tokenResponse.AccessToken);
            customers = final.ToList();
            if (customers.Any()) Console.WriteLine("Finalized Customers");
            foreach (var c in customers)
            {
                Console.WriteLine($"Customer => {c}");
            }
            Console.WriteLine("");

            Console.ReadKey();
        }
    }
}
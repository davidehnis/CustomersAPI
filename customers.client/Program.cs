using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System;
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

            // Create Customers API client
            var customersApiClient = new HttpClient();
            customersApiClient.SetBearerToken(tokenResponse.AccessToken);

            var response = await customersApiClient.GetAsync("https://localhost:44363/identity");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(JArray.Parse(content));

                var customers = await customersApiClient.GetAsync("https://localhost:44363/api/Customers");
                //customersApiClient.Send()
            }

            Console.ReadKey();
        }
    }
}
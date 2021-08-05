using IdentityModel.Client;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace customers.client
{
    public class CustomersApiClient
    {
        public async Task<IEnumerable<Customer>> Fetch(string token)
        {
            var customersApiClient = new HttpClient();
            customersApiClient.SetBearerToken(token);

            var response = await customersApiClient.GetAsync("https://localhost:44363/identity");
            if (!response.IsSuccessStatusCode)
            {
                return new List<Customer>();
            }

            var result = await customersApiClient.GetAsync("https://localhost:44363/api/Customers");
            var data = await result.Content.ReadAsStringAsync();
            var customers = JsonConvert.DeserializeObject<List<Customer>>(data);
            return customers;
        }

        public async Task<Customer> Get(string token, int id)
        {
            var customersApiClient = new HttpClient();
            customersApiClient.SetBearerToken(token);

            var response = await customersApiClient.GetAsync("https://localhost:44363/identity");
            if (!response.IsSuccessStatusCode)
            {
                return new Customer();
            }

            var result = await customersApiClient.GetAsync("https://localhost:44363/api/Customers/Get/1");
            var data = await result.Content.ReadAsStringAsync();
            var customer = JsonConvert.DeserializeObject<Customer>(data);
            return customer;
        }

        public async Task<HttpResponseMessage> Save(string token, Customer customer)
        {
            var customersApiClient = new HttpClient();
            customersApiClient.SetBearerToken(token);

            var response = await customersApiClient.GetAsync("https://localhost:44363/identity");
            if (!response.IsSuccessStatusCode)
            {
                return new HttpResponseMessage(HttpStatusCode.Forbidden);
            }

            var content = JsonConvert.SerializeObject(customer);
            var data = new StringContent(content, Encoding.UTF8, "application/json");
            return await customersApiClient.PostAsync("https://localhost:44363/api/Customers", data);
        }
    }
}
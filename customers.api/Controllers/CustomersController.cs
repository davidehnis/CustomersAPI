using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace customers.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomersController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Customer> Get()
        {
            return CustomerDatabase.Fetch();
        }

        [HttpGet("[action]/{id}")]
        public Customer Get(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException($"Customers Ids have to be larger than zero. Id '{id}' is not compatible");
            }

            var customers = CustomerDatabase.Fetch();
            return customers.FirstOrDefault(c => c.Id == id.ToString());
        }

        [HttpPost]
        public void Save([FromBody] Customer customer)
        {
            if (customer == null)
            {
                throw new ArgumentException($"Customer was invalid");
            }

            CustomerDatabase.Save(customer);
        }
    }
}
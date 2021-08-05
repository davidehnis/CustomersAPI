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

        //[HttpGet]
        //public Customer Get(int id)
        //{
        //    var customers = CustomerDatabase.Fetch();
        //    return customers.FirstOrDefault(c => c.Id == id.ToString());
        //}

        [HttpPost]
        public void Save(Customer customer)
        {
            CustomerDatabase.Save(customer);
        }
    }
}
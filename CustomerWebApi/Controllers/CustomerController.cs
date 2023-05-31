using CustomerWebApi.Data;
using CustomerWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomerWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
            private readonly CustomerDbContext _customerDbContext;
        public CustomerController(CustomerDbContext customerDbContext)
        {
           _customerDbContext = customerDbContext;
        }

        [HttpGet]
        public ActionResult <IEnumerable<Models.Customer>> GetAll ()
        {
            return _customerDbContext.Customers.ToList();
        }

        [HttpGet("{customerId:int}")]
        public async Task<ActionResult<Customer>> GetById(int customerId)
        {
            var customer = await _customerDbContext.Customers.FindAsync(customerId);
            return customer;
        }

        [HttpPost]
        public async Task<ActionResult> Create(Customer customer)
        {
            await _customerDbContext.Customers.AddAsync(customer);
            await _customerDbContext.SaveChangesAsync();
            return Ok();
            }

            //[HttpPut]
            //public async Task<ActionResult> Update(Customer customer)
            //{
            //    _customerDbContext.Customers.Update(customer);
            //    await _customerDbContext.SaveChangesAsync();
            //    return Ok();
            //  }
        }
}

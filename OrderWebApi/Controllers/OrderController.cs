using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderWebApi.Datas;
using OrderWebApi.Models;

namespace OrderWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderDbContext _orderDbContext;
        public OrderController(OrderDbContext orderDbContext)
        {
            _orderDbContext = orderDbContext;
        }


        [HttpGet]
        public ActionResult<IEnumerable<Models.Order>> GetAll()
        {
            var b = _orderDbContext;
            return _orderDbContext.Orders.ToList();
        }

        [HttpGet("{orderId:int}")]
        public async Task<ActionResult<Order>> GetById(int orderId)
        {
            var oder = await _orderDbContext.Orders.FindAsync(orderId);
            return oder;
        }

        [HttpPost]
        public async Task<ActionResult> Create(Order customer)
        {
            await _orderDbContext.Orders.AddAsync(customer);
            await _orderDbContext.SaveChangesAsync();
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

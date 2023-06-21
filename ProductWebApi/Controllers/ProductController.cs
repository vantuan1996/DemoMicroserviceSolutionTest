using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductWebApi.Data;
using ProductWebApi.Handlers;
using ProductWebApi.Models;
using ProductWebApi.Queries;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ProductWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductDbContext _dbContext;
        private readonly IMediator _mediator;
        public ProductController(ProductDbContext productDbContext, IMediator mediator)
        {
            _dbContext = productDbContext;
            _mediator = mediator;
        }
        [HttpGet("getAll")]
        public async Task<ActionResult> GetProducts()
        {
            var products = await _mediator.Send(new GetProductsQuery());
            return Ok(products);
        }
        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetAllProducts()
        {
            return _dbContext.Products;
        }

        [HttpGet("{productId:int}")]
        public async Task<ActionResult<Product>> GetById(int id)
        {
            var product = await _mediator.Send(new GetProductByIdQuery(id));
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody]  Product product)
        {
            var productToReturn = await _mediator.Send(new AddProductCommand(product));
            //return CreatedAtRoute("GetProductById", new { id = productToReturn.ProductId }, productToReturn);
            //await _dbContext.Products.AddAsync(product);
            //await _dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody]  Product product)
        {
            var productToReturn = await _mediator.Send(new UpdateProductCommand(product));
            //_dbContext.Products.Update(product);
            //await _dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{productId:int}")]
        public async Task<ActionResult> Delete(int productId)
        {
            var product = await _dbContext.Products.FindAsync(productId);
            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}

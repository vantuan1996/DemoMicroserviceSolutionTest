using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductWebApi.Data;
using ProductWebApi.Models;
using ProductWebApi.Queries;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ProductWebApi.Handlers
{

    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Product>
    {
        
        private readonly ProductDbContext _dbProductContext;
        public GetProductByIdQueryHandler(ProductDbContext dbProductContext)
        {
            _dbProductContext = dbProductContext;
        }
        public  async Task<Product> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            
            var product =   _dbProductContext.Products.Where(a => a.ProductId == request.Id).FirstOrDefault();
            if (product == null) return null;
            return  product;
        }
    }
}

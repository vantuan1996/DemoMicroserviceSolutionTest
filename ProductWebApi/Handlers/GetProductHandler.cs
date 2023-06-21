using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductWebApi.Data;
using ProductWebApi.Models;
using ProductWebApi.Queries;

namespace ProductWebApi.Handlers
{
    public class GetProductsHandler : IRequestHandler<GetProductsQuery, IEnumerable<Product>>
    {
        private readonly ProductDbContext _dbContext;
        public GetProductsHandler(ProductDbContext dbContext) => _dbContext = dbContext;

        //public async Task<IEnumerable<Product>> Handle(GetProductsQuery request,
        //    CancellationToken cancellationToken) => await _dbContext.GetAllProducts();
        public async Task<IEnumerable<Product>> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {
            var productList = await _dbContext.Products.ToListAsync();
            if (productList == null)
            {
                return null;
            }
            return productList.AsReadOnly();
        }
    }

    //Thêm mới
    public class AddProductHandler : IRequestHandler<AddProductCommand, Product>
    {
        private readonly ProductDbContext _dbContext;
        public AddProductHandler(ProductDbContext dbContext) => _dbContext = dbContext;
        public async Task<Product> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            await _dbContext.Products.AddAsync(request.Product);
            await _dbContext.SaveChangesAsync();
            return request.Product;
        }
    }

    // Cập nhật

    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, Product>
    {
        private readonly ProductDbContext _dbContext;
        public UpdateProductHandler(ProductDbContext dbContext) => _dbContext = dbContext;

        public async Task<Product> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = _dbContext.Products.Where(a => a.ProductId == request.Product.ProductId).FirstOrDefault();
    
            if (product == null) return null;
            product.ProductName = request.Product.ProductName;
            product.ProductPrice = request.Product.ProductPrice;
            product.ProductCode = request.Product.ProductCode;
            _dbContext.Products.Update(product);
            await _dbContext.SaveChangesAsync();
            return request.Product;
        }
      


    }
}

using ProductWebApi.Models;

namespace ProductWebApi
{
    public class FakeDataStore
    {
        private static List<Product> _products;
        public FakeDataStore()
        {
            _products = new List<Product>
        {
            new Product { ProductId = 1, ProductName = "Test Product 1" , ProductCode = "P001",ProductPrice = 100 },
            new Product { ProductId = 2, ProductName = "Test Product 2" , ProductCode = "P002" ,ProductPrice = 120 },
            new Product { ProductId = 3, ProductName = "Test Product 3" , ProductCode = "P003" ,ProductPrice = 150}
        };
        }
        public async Task AddProduct(Product product)
        {
            _products.Add(product);
            await Task.CompletedTask;
        }
        public async Task<IEnumerable<Product>> GetAllProducts() => await Task.FromResult(_products);
        public async Task EventOccured(Product product, string evt)
        {
            _products.Single(p => p.ProductId == product.ProductId).ProductName = $"{product.ProductName} evt: {evt}";
            await Task.CompletedTask;
        }
    }

}

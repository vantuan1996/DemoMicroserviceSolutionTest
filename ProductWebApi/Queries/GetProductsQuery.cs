using MediatR;
using ProductWebApi.Models;

namespace ProductWebApi.Queries
{
    public record GetProductsQuery() : IRequest<IEnumerable<Product>>;
    public record GetProductByIdQuery(int Id) : IRequest<Product>;  
    public record AddProductCommand(Product Product) : IRequest<Product>;
    public record UpdateProductCommand(Product Product) : IRequest<Product>;
    public record ProductAddedNotification(Product Product) : INotification;
}

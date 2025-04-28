
using BuildingBlocks.CQRS;
using Catalog.API.Models;

namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(string Name, string Description, string ImageFile, decimal Price, List<string> Category) 
        : ICommand<CreateProductResponse>;
    public record class CreateProductResponse(Guid Id);
    public class CreateProductHandler : ICommandHandler<CreateProductCommand, CreateProductResponse>
    {
        public async Task<CreateProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            // Create product entity
            var newProduct = new Product
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                ImageFile = request.ImageFile,
                Price = request.Price,
                Category = request.Category
            };

            // Save to db

            // Return the created product ID
            return new CreateProductResponse(newProduct.Id);
        }
    }
}

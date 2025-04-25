using MediatR;

namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(string Name, string Description, string ImageFile, decimal Price, List<string> Category) : IRequest<CreateProductResponse>;
    public record class CreateProductResponse(Guid Id);
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, CreateProductResponse>
    {
        public Task<CreateProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            return null;
        }
    }
}

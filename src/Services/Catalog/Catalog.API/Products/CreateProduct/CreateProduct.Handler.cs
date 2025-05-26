
namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(string Name, string Description, string ImageFile, decimal Price, List<string> Category) 
        : ICommand<CreateProductResult>;
    public record class CreateProductResult(Guid Id);

    public class CreateProductValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required.");
        }
    }
    internal class CreateProductHandler(IDocumentSession session) : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            // Create product entity
            var newProduct = new Product
            {
                Name = request.Name,
                Description = request.Description,
                ImageFile = request.ImageFile,
                Price = request.Price,
                Category = request.Category
            };

            // Save to db
            session.Store(newProduct);
            await session.SaveChangesAsync(cancellationToken);

            // Return the created product ID
            return new CreateProductResult(newProduct.Id);
        }
    }
}

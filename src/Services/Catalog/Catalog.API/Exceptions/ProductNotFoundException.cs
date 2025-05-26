namespace Catalog.API.Exceptions
{
    public class ProductNotFoundException : NotFoundException<Product>
    {
        public ProductNotFoundException(Guid id) : base(id)
        {
        }
    }
}

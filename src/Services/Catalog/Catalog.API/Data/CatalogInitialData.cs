using Marten.Schema;

namespace Catalog.API.Data
{
    public class CatalogInitialData : IInitialData
    {
        public async Task Populate(IDocumentStore store, CancellationToken cancellation)
        {
            using var session = store.LightweightSession();

            if (await session.Query<Product>().AnyAsync(cancellation))
            {
                return; // Data already exists, no need to seed
            }

            session.Store(GetPredefinedProducts());
            await session.SaveChangesAsync(cancellation);
        }

        private static IEnumerable<Product> GetPredefinedProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Product 1",
                    Description = "Description for Product 1",
                    Price = 10.99m,
                    ImageFile = "product1.jpg",
                    Category = new List<string> { "Category1", "Category2" }
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Product 2",
                    Description = "Description for Product 2",
                    Price = 20.99m,
                    ImageFile = "product2.jpg",
                    Category = new List<string> { "Category2", "Category3" }
                }
            };
        }
    }

}

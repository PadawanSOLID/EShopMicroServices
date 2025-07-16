using Catalog.API.Models;
using Marten.Schema;

namespace Catalog.API.Data
{
    public class CatalogInitialData : IInitialData
    {
        public async Task Populate(IDocumentStore store, CancellationToken cancellation)
        {
            using var session=store.LightweightSession();

            if (await session.Query<Product>().AnyAsync())
            {
                return;
            }

            session.Store<Product>(GetPreconfiguredProducts());
            await session.SaveChangesAsync(cancellation);
        }

        private static IEnumerable<Product> GetPreconfiguredProducts()
        {
            return [
                new Product()
                {
                    Name="Iphone X",
                    Description="test",
                    Category=["Phone"],
                    Price=850,
                    ImageFile="iphonex.png"
                }
                ];
        }
    }
}

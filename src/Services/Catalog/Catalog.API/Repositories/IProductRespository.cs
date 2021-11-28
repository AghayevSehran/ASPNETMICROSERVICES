using Catalog.API.Enities;

namespace Catalog.API.Repositories
{
    public interface IProductRespository
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<Product> GetProduct(string id);
        Task<IEnumerable<Product>> GetProductByName(string name);
        Task<IEnumerable<Product>> GetProductByCategory(string categoryName);
        Task<bool> UpdateProduct(Product product);
        Task<bool> DeleteProduct(string id);
        Task CreateProduct(Product product);
    }
}

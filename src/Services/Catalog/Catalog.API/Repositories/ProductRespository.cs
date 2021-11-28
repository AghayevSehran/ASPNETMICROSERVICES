﻿using Catalog.API.Data;
using Catalog.API.Enities;
using MongoDB.Driver;

namespace Catalog.API.Repositories
{
    public class ProductRespository : IProductRespository
    {
        private readonly ICatalogContext _context;
        public ProductRespository(ICatalogContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _context.Products.Find(c => true).ToListAsync();
        }
        public async Task<IEnumerable<Product>> GetProductByCategory(string categoryName)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq
              (c => c.Category, categoryName);
            return await _context.Products.Find(filter).ToListAsync();
        }
        public async Task<IEnumerable<Product>> GetProductByName(string name)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq
                (c => c.Name, name);
            return await _context.Products.Find(filter).ToListAsync();
        }    
        public async Task CreateProduct(Product product)
        {
            await _context.Products.InsertOneAsync(product);
        }
        public async Task<bool> DeleteProduct(string id)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq
             (c => c.Id, id);

            var deleteResult = await _context.Products
               .DeleteOneAsync(filter);
            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }
        public async Task<bool> UpdateProduct(Product product)
        {
            var updateResult = await _context.Products
                .ReplaceOneAsync(f => f.Id == product.Id, product);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }
        public async Task<Product> GetProduct(string id)
        {
            return await _context
                .Products
                .Find(c => c.Id == id).FirstOrDefaultAsync();
        }
    }
}

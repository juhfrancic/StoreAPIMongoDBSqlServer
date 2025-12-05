using MongoDB.Bson;
using MongoDB.Driver;
using TomadaStore.Models.DTOs.Category;
using TomadaStore.Models.DTOs.Products;
using TomadaStore.Models.Models;
using TomadaStoreProductAPI.Data;
using TomadaStoreProductAPI.Repositories.Interfaces;

namespace TomadaStoreProductAPI.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ILogger<ProductRepository> _logger;
        private readonly IMongoCollection<Product> _mongoCollection;
        private readonly ConnectionDB _connection;

        public ProductRepository(ILogger<ProductRepository> logger, ConnectionDB connection)
        {
            _logger = logger;
            _connection = connection;
            _mongoCollection = _connection.GetMongoConnection();
        }
       
        public async Task CreateProductAsync(ProductRequestDTO productDTO)
        {
            try
            {
                await _mongoCollection.InsertOneAsync(new Product
                (
                    productDTO.Name,
                    productDTO.Description,
                    productDTO.Price,
                    new Category(
                        productDTO.Category.ToString(),
                        productDTO.Category.Name,
                        productDTO.Category.Description)
                ));
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error creating product: {ex.Message}");
                throw;
            }
        }

        public async Task<ProductResponseDTO> GetProductByIbAsync(string id)
        {
            try
            {
                var product = await _mongoCollection.Find(d => d.Id == new ObjectId(id)).FirstOrDefaultAsync();
                ProductResponseDTO productDto = new()
                {
                    Id = product.Id.ToString(),
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Category = new CategoryResponseDTO
                    {
                        Id = product.Id.ToString(),
                        Name = product.Name,
                        Description = product.Description
                    }
                };
                return productDto;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error getting product by id: {ex.Message}");
                throw;
            }
        }

        public Task<List<ProductResponseDTO>> GetAllProductsAsync()
        {
            throw new NotImplementedException();
        }
    }
}

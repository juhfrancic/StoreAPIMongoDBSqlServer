using TomadaStore.Models.DTOs.Products;
using TomadaStoreProductAPI.Repositories.Interfaces;
using TomadaStoreProductAPI.Services.Interfaces;

namespace TomadaStoreProductAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly ILogger<ProductService> _logger;
        private readonly IProductRepository  _productRepository;

        public  ProductService(ILogger<ProductService> logger, IProductRepository productRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
        }

        public async Task CreateProductAsync(ProductRequestDTO productDto)
        {
            try
            {
                await _productRepository.CreateProductAsync(productDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating product");
                throw;
            }
        }

        public Task DeleteProduct(string id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductResponseDTO>> GetAllProductsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ProductResponseDTO> GetProductByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateProduct(string id, ProductRequestDTO productDto)
        {
            throw new NotImplementedException();
        }
    }
}

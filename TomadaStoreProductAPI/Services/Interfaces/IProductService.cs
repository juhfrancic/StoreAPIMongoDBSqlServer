using TomadaStore.Models.DTOs.Products;

namespace TomadaStoreProductAPI.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductResponseDTO>> GetAllProductsAsync();
        Task<ProductResponseDTO> GetProductByIdAsync(string id);
        Task CreateProductAsync(ProductRequestDTO productDto);
        Task UpdateProduct(string id, ProductRequestDTO productDto);
        Task DeleteProduct(string id);
    }
}
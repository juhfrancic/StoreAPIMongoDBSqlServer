using TomadaStore.Models.DTOs.Products;

namespace TomadaStoreProductAPI.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task CreateProductAsync(ProductRequestDTO productDto);
        Task<ProductResponseDTO> GetProductByIbAsync(string id);
        Task<List<ProductResponseDTO>> GetAllProductsAsync();
    }
}
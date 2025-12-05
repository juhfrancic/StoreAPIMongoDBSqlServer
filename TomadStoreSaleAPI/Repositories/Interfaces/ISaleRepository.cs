using TomadaStore.Models.DTOs.Customer;
using TomadaStore.Models.DTOs.Products;
using TomadaStore.Models.DTOs.Sale;
using TomadaStore.Models.Models;

namespace TomadStoreSaleAPI.Repositories.Interfaces
{
    public interface ISaleRepository
    {
        Task CreateSaleAsync(CustomerResponseDTO custumerDto, ProductResponseDTO productDto, SaleRequestDTO saleDto);
        Task<List<Sale>> GetAllSalesAsync();
    }
}

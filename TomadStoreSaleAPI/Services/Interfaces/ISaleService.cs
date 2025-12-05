using TomadaStore.Models.DTOs.Sale;

namespace TomadStoreSaleAPI.Services.Interfaces
{
    public interface ISaleService
    {
        Task CreateSaleAsync(int idCustomer, string idProduct, SaleRequestDTO saleDTO);
        Task<List<SaleResponseDTO>> GetAllSalesAsync();
    }
}

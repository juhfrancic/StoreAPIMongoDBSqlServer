using TomadaStore.Models.DTOs.Customer;
using TomadaStore.Models.Models;

namespace TomadaStoreCustomerAPI.Services
{
    public interface ICustomerService
    {
        Task InsertCustomerAsync(CustomerRequestDTO custumer);
        Task<List<CustomerResponseDTO>> GetAllCustomersAsync();
        Task<CustomerResponseDTO> GetCustomerByIdAsync(int id);
        Task UpdateSituacao(int id);
    }
}

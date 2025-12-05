using TomadaStore.Models.DTOs.Customer;
using TomadaStore.Models.Models;

namespace TomadaStoreCustomerAPI.Repositories
{
    public interface ICustumerRepository
    {
        Task InsertCustomerAsync(CustomerRequestDTO customer);
        Task<List<CustomerResponseDTO>> GetAllCustomerAsync();
        Task<CustomerResponseDTO> GetCustomerByIdAsync(int id);
        Task UpdateSituacao(int id);
    }

}

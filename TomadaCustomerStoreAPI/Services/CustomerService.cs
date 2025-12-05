using TomadaStore.Models.DTOs.Customer;
using TomadaStore.Models.Models;
using TomadaStoreCustomerAPI.Repositories;

namespace TomadaStoreCustomerAPI.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ILogger<CustomerService> _logger;
        private readonly ICustumerRepository _customerRepository;   

        public CustomerService(ILogger<CustomerService> logger,ICustumerRepository customerRepository)
        {
            _logger = logger;
            _customerRepository = customerRepository;
        }

        public async Task<List<CustomerResponseDTO>> GetAllCustomersAsync()
        {
            try
            {
                return await _customerRepository.GetAllCustomerAsync();
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<CustomerResponseDTO> GetCustomerByIdAsync(int id)
        {
            try
            {
                return await _customerRepository.GetCustomerByIdAsync(id);
            }
            catch(Exception ex)
            {
                throw new Exception("Error getting customer by id", ex);
            }
        }

        public async Task InsertCustomerAsync(CustomerRequestDTO customer)
        {
            try
            {
               await _customerRepository.InsertCustomerAsync(customer);
            }
            catch(Exception ex)
            {
                throw new Exception("Error inserting customer", ex);
            }
        }

        public async Task UpdateSituacao(int id)
        {
            try
            {
                await _customerRepository.UpdateSituacao(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating situation", ex);
            }
        }
    }
}

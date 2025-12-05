using MongoDB.Driver;
using TomadaStore.Models.DTOs.Customer;
using TomadaStore.Models.DTOs.Products;
using TomadaStore.Models.DTOs.Sale;
using TomadaStore.Models.Models;
using TomadaStoreSaleAPI.Data;
using TomadStoreSaleAPI.Repositories.Interfaces;

namespace TomadStoreSaleAPI.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        private readonly ILogger<SaleRepository> _logger; 
        private readonly IMongoCollection<Sale> _saleCollection;
        private readonly ConnectionDBSale _connection;

        public SaleRepository(ILogger<SaleRepository> logger,
                                ConnectionDBSale connection)
        {
            _logger = logger;
            _connection = connection;
            _saleCollection = _connection.GetMongoCollection();
        }
        public async Task CreateSaleAsync(CustomerResponseDTO customerDTO,
                                            ProductResponseDTO productDTO,
                                            SaleRequestDTO sale)
        {
            try
            {
                var products = new List<Product>();

                var category = new Category
                (
                    productDTO.Category.Id,
                    productDTO.Category.Name,
                    productDTO.Category.Description
                );

                var product = new Product
                (
                    productDTO.Id,
                    productDTO.Name,
                    productDTO.Description,
                    productDTO.Price,
                    category
                );

                products.Add(product);

                var customer = new Customer
                (
                    customerDTO.Id,
                    customerDTO.FirstName,
                    customerDTO.LastName,
                    customerDTO.Email,
                    customerDTO.PhoneNumber
                );
                await _saleCollection.InsertOneAsync(new Sale
                (
                    customer,
                    products,
                    productDTO.Price
                ));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error creating sale: {ex.Message}");
                throw;
            }
        }

        public async Task<List<Sale>> GetAllSalesAsync()
        {
            try
            {
                return await _saleCollection.FindAsync(_ => true).Result.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error getting sales: {ex.Message}");
                throw;
            }
        }
    }
}

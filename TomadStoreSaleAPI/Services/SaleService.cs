using TomadaStore.Models.DTOs.Category;
using TomadaStore.Models.DTOs.Customer;
using TomadaStore.Models.DTOs.Products;
using TomadaStore.Models.DTOs.Sale;
using TomadaStore.Models.Models;
using TomadStoreSaleAPI.Repositories.Interfaces;
using TomadStoreSaleAPI.Services.Interfaces;

namespace TomadStoreSaleAPI.Services
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _saleRepository;
        private readonly ILogger<SaleService> _logger;
        private readonly HttpClient _httpClientProduct;
        private readonly HttpClient _httpClientCustomer;

        public SaleService(ISaleRepository saleRepository, ILogger<SaleService> logger,
                           HttpClient httpProduct, HttpClient httpCustomer)
        {
            _saleRepository = saleRepository;
            _logger = logger;
            _httpClientProduct = httpProduct;
            _httpClientCustomer = httpCustomer;
        }

        public async Task CreateSaleAsync(int idCustomer, string idProduct, SaleRequestDTO saleDTO)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error ocurred");
                throw;
            }
        }
    }
}

//        public async Task<List<SaleResponseDTO>> GetAllSalesAsync()
//        {
//            try
//            {
//                var sales = await _saleRepository.GetAllSalesAsync();
//                var saleDto = new List<SaleResponseDTO>();
//                foreach (var s in sales)
//                {
//                    var listProduct = new List<ProductResponseDTO>();
//                    foreach (var p in s.Products)
//                    {
//                        var productDTO = new ProductResponseDTO
//                        {
//                            Id = p.Id.ToString(),
//                            Name = p.Name,
//                            Description = p.Description,
//                            Price = p.Price,
//                            Category = new CategoryResponseDTO
//                            {
//                                Id = p.Category.Id.ToString(),
//                                Name = p.Category.Name,
//                                Description = p.Category.Description,
//                            }
//                        };
//                        listProduct.Add(productDTO);
//                    }

//                    var saleResponseDTO = new SaleResponseDTO
//                    {
//                        Id = s.Id.ToString(),
//                        Customer = new CustomerResponseDTO
//                        {
//                            Id = s.Customer.Id,
//                            FirstName = s.Customer.FirstName,
//                            LastName = s.Customer.LastName,
//                            Email = s.Customer.Email,
//                            PhoneNumber = s.Customer.PhoneNumber,
//                            Situacao = s.Customer.Situacao
//                        },
//                        Products = listProduct,
//                        TotalPrice = s.TotalPrice,
//                    };
//                    saleDto.Add(saleResponseDTO);
//                }
//                return saleDto;
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError($"Error getting all sales: {ex.Message}");
//                throw;
//            }
//        }
//    }
//}



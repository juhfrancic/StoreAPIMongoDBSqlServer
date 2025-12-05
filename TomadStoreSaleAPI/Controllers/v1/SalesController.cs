using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TomadaStore.Models.DTOs.Sale;
using TomadStoreSaleAPI.Services.Interfaces;

namespace TomadStoreSaleAPI.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly ILogger<SaleController> _logger;
        private readonly ISaleService _saleService;


        public SaleController(ILogger<SaleController> logger,
                                ISaleService saleService)
        {
            _logger = logger;
            _saleService = saleService;
        }

        [HttpPost("customer/{idCustomer}/product/{idProduct}")]
        public async Task<IActionResult> CreateSaleAsync(int idCustomer,
                                                        string idProduct,
                                                        [FromBody] SaleRequestDTO saleDTO)
        {
            _logger.LogInformation("Creating a new sale.");

            await _saleService.CreateSaleAsync(idCustomer, idProduct, saleDTO);

            return Ok("Sale created successfully.");
        }
    }
}

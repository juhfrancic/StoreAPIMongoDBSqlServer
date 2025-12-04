using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using TomadaStore.Models.DTOs;
using TomadaStore.Models.Models;
using TomadaStoreCustomerAPI.Services;

namespace TomadaStoreCustomerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly ICustomerService _customerService;
        
        public CustomerController(ILogger<CustomerController> logger, ICustomerService customerService)
        {
            _logger = logger;
            _customerService = customerService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateCostumer(CustomerRequestDTO customer)
        {
            try
            {
                _logger.LogInformation("Creating a new costumer");
                await _customerService.InsertCustomerAsync(customer);
                return Created();
            }
            catch(Exception e)
            {
                _logger.LogError(e, "Error occured while creating a new customer." + e.Message);

                return Problem(e.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<CustomerResponseDTO>>> GetAllCustomerAsync()
        {
            try
            {
                var customer = await _customerService.GetAllCustomersAsync();
                return customer;
            }
            catch(Exception e)
            {
                _logger.LogError(e, "Error occured while getting a customer." + e.Message);

                return Problem(e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerResponseDTO>> GetCustomerByIdAsync(int id)
        {
            try
            {
                var customer = await _customerService.GetCustomerByIdAsync(id);
                return customer;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error occured while getting a customer." + e.Message);

                return Problem(e.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateSituacao(int id)
        {
            try
            {
                await _customerService.UpdateSituacao(id);
                return Ok();
            }
            catch(Exception e)
            {
                _logger.LogError(e, "Error occured while getting a customer." + e.Message);

                return Problem(e.Message);
            }
        }
    }
}

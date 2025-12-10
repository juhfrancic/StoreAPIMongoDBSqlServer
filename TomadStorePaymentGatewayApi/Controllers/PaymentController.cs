using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using TomadaStore.Models.DTOs.Payment;
using TomadaStore.Models.DTOs.Sale;
using TomadStorePaymentGatewayApi.Services;

namespace TomadStorePaymentGatewayApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly PaymentGatewayService _paymentService;

        public PaymentController(IConfiguration configuration, PaymentGatewayService paymentService)
        {
            _configuration = configuration;
            _paymentService = paymentService;
        }

        [HttpPost]
        public async Task<ActionResult> PaymentConsumeAsync()
        {

            try
            {
                await _paymentService.PaymentConsumeAsync();
                return Ok("Message sent to RabbitMQ");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}

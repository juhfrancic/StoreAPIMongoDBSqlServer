using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using System.Data.Common;
using System.Text;
using System.Text;
using System.Text.Json;
using System.Threading.Channels;
using TomadaStore.Models.DTOs.Sale;
using RabbitMQ.Client;
using static MongoDB.Driver.WriteConcern;

namespace TomadStoreSaleAPI.Controllers.v2
{
    [Route("api/v2/[controller]")]
    [ApiController]
    public class SalesProducerController : ControllerBase
    {

        [HttpPost]
        public async Task<ActionResult> SaleProduceRequest([FromBody] SaleRequestDTO sale)
        {

            try
            {
                var factory = new ConnectionFactory() { HostName = "localhost" };
                using var connection = await factory.CreateConnectionAsync();
                using var channel = await connection.CreateChannelAsync();

                {
                    await channel.QueueDeclareAsync(queue: "salesQueue",
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);
                    var saleMessage = JsonSerializer.Serialize<SaleRequestDTO>(sale);
                    var body = Encoding.UTF8.GetBytes(saleMessage);

                    await channel.BasicPublishAsync(exchange: "",
                                         routingKey: "salesQueue",
                                         body: body);

                    Console.WriteLine($" [x] Sent {saleMessage}");

                    Console.WriteLine(" Press [enter] to exit.");
                }
                return Ok("Message sent to RabbitMQ");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        private int SaleRequestDTO(SaleRequestDTO sale)
        {
            throw new NotImplementedException();
        }
    }
}


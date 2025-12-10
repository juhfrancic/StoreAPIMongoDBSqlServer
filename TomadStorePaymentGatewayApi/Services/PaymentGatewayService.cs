using Microsoft.AspNetCore.Connections;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System.Text;
using System.Text.Json;
using TomadaStore.Models.DTOs.Payment;
using TomadaStore.Models.DTOs.Sale;
using TomadaStore.Models.Models;




namespace TomadStorePaymentGatewayApi.Services
{
    public class PaymentGatewayService
    {
        private IConnection _connection;
        private IConfiguration _configuration;
        private IChannel _producerChannel;
        private IChannel _consumerChannel;

        public PaymentGatewayService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task PaymentConsumeAsync()
        {
            var factory = new ConnectionFactory()
            {
                HostName = _configuration["RabbitMQ:HostName"],
                UserName = _configuration["RabbitMQ:UserName"],
                Password = _configuration["RabbitMQ:Password"]
            };

            _connection = await factory.CreateConnectionAsync();
            _consumerChannel = await _connection.CreateChannelAsync();
            _producerChannel = await _connection.CreateChannelAsync();

            await _consumerChannel.QueueDeclareAsync(queue: "salesQueue",
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

            var consumer = new AsyncEventingBasicConsumer(_consumerChannel);
            consumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                try
                {
                    var saleRequest = JsonSerializer.Deserialize<SaleRequestDTO>(message);

                    if (saleRequest != null)
                    {
                        Console.WriteLine($"Received Sale: SaleId={saleRequest.SaleId}, totalPrice={saleRequest.TotalPrice}");


                        var paymentDto = new PaymentDTO
                        {
                            SaleId = saleRequest.SaleId,
                            ProductId = saleRequest.ProductId,
                            TotalPrice = saleRequest.TotalPrice,
                            Situacao = ValidateSale(saleRequest.TotalPrice)
                        };


                        await PaymentProduceAsync(paymentDto);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error Processing message: {ex.Message}");
                    await _consumerChannel.BasicNackAsync(ea.DeliveryTag, false, true);
                }
            };

            await _consumerChannel.BasicConsumeAsync(
                queue: "salesQueue",
                autoAck: false,
                consumer: consumer
            );

            Console.WriteLine("Payment Gateway waiting for sales...");

        }

        public string ValidateSale(decimal totalPrice)
        {
            if (totalPrice <= 0 || totalPrice >= 1000)
                return "Rejected";

            return "Approved";
        }


        public async Task PaymentProduceAsync(PaymentDTO payment)
        {
            try
            {
                var factory = new ConnectionFactory() { HostName = "localhost" };
                using var connection = await factory.CreateConnectionAsync();
                using var channel = await connection.CreateChannelAsync();

                await channel.QueueDeclareAsync(queue: "paymentQueue",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);
                var paymentMessage = JsonSerializer.Serialize<PaymentDTO>(payment);
                var body = Encoding.UTF8.GetBytes(paymentMessage);

                await channel.BasicPublishAsync(exchange: "",
                                     routingKey: "paymentQueue",
                                     body: body);
            }
            catch (RabbitMQClientException ex)
            {
                throw new Exception(ex.Message);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}


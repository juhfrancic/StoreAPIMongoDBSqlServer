using MongoDB.Driver;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using TomadaStore.Models.DTOs.Payment;
using TomadaStore.Models.Models;

namespace StoreConsumerAPI.Services
{
    public class SaleConsumerService
    {
        private readonly IConfiguration _configuration;
        private readonly IMongoCollection<SaleMongo> _salesCollection;
        private IConnection _connection;
        private IChannel _channel;

        public SaleConsumerService(IConfiguration configuration)
        {
            _configuration = configuration;
            var mongoClient = new MongoClient(_configuration["MongoDB:ConnectionURI"]);
            var database = mongoClient.GetDatabase(_configuration["MongoDB:DatabaseName"]);
            _salesCollection = database.GetCollection<SaleMongo>("Sales");
        }

        public async Task PaymentConsumeAsync()
        {
            var factory = new ConnectionFactory()
            {
                HostName = _configuration["RabbitMQ:HostName"] ?? "localhost"
            };

            _connection = await factory.CreateConnectionAsync();
            _channel = await _connection.CreateChannelAsync();

            await _channel.QueueDeclareAsync(queue: "paymentQueue",
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                try
                {
                    var paymentDto = JsonSerializer.Deserialize<PaymentDTO>(message);

                    if (paymentDto != null)
                    {
                        Console.WriteLine($"Received payment: {paymentDto.SaleId}");


                        var saleMongo = new SaleMongo
                        {
                            SaleId = paymentDto.SaleId,
                            ProductId = paymentDto.ProductId,
                            TotalPrice = paymentDto.TotalPrice,
                            Situacao = paymentDto.Situacao
                        };


                        await _salesCollection.InsertOneAsync(saleMongo);

                        Console.WriteLine($"Sale saved to MongoDB: {paymentDto.SaleId}");

                        await _channel.BasicAckAsync(ea.DeliveryTag, false);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error Processing message: {ex.Message}");
                    await _channel.BasicNackAsync(ea.DeliveryTag, false, true);
                }
            };
            await _channel.BasicConsumeAsync(
                queue: "paymentQueue",
                autoAck: false,
                consumer: consumer
            );
            Console.WriteLine("[*] Sale Consumer waiting for approved payments...");
        }
    }
    }


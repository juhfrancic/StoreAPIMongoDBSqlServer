using StoreConsumerAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddSingleton<SaleConsumerService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

var consumerService = app.Services.GetRequiredService<SaleConsumerService>();
_ = consumerService.PaymentConsumeAsync();

app.Run();

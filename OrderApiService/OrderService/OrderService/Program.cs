using OrderService;
using Plain.RabbitMQ;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Order service",
        Version = "v1"
    });
});

var connectionString = builder.Configuration["ConnectionString"];
builder.Services.AddSingleton<IOrderDetailsProvider>(new OrderDetailsProvider(connectionString));
builder.Services.AddSingleton<IOrderCreator>(x => new OrderCreator(connectionString, x.GetService<ILogger<OrderCreator>>()));
builder.Services.AddSingleton<IOrderDeletor>(new OrderDeletor(connectionString));

builder.Services.AddSingleton<IConnectionProvider>(new ConnectionProvider("amqp://guest:guest@localhost:5672"));
builder.Services.AddSingleton<IPublisher>(x => new Publisher(x.GetService<IConnectionProvider>(),
        "order_exchange",
        ExchangeType.Topic));
builder.Services.AddSingleton<ISubscriber>(x => new Subscriber(x.GetService<IConnectionProvider>(),
    "inventory_exchange",
    "inventory_response",
    "inventory.response",
    ExchangeType.Topic));

builder.Services.AddHostedService<InventoryResponseListener>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}


app.UseSwagger();
app.UseSwaggerUI(c => {
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Order Service");
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

using MassTransit;
using OrderApi.RabbitMq;
using OrderBus.Consumers.LocalConsumers;
using OrderBus.Contracts;
using OrderBus.Queues;
using OrderService.Service.Base;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<MakeOrderConsumer>();
    x.UsingRabbitMq((ctx, config) =>
    {
        config.Host(RabbitMqConsts.RabbitMqRootUri + $"{RabbitMqConsts.VirtualHost}", h =>
        {
            h.Username(RabbitMqConsts.UserName);
            h.Password(RabbitMqConsts.Password);
        });
        config.ReceiveEndpoint(OrderContractQueue.NotificationQueueNameOrder, ep =>
        {
            ep.ConfigureConsumer<MakeOrderConsumer>(ctx);
        });
        config.AutoStart = true;
    });
    x.AddRequestClient<OrderContract>();
});

builder.Services.AddTransient<IOrderService, OrderService.Service.OrderService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(options =>
{
    options.
    AllowAnyMethod().
    AllowAnyHeader().
    SetIsOriginAllowed(origin => true).
    AllowCredentials();

});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

using SendToMailServices.Services;
using SendToMailServices.Services.Base;
using MassTransit;
using SendToMailBus.MassTransit.Consumers;
using SendToMailApi.RabbitMq;
using SendToMailBus.MassTransit.Queues;
using SendToMailBus.MassTransit.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<SendToMailConsumer>();
    x.UsingRabbitMq((ctx, config) =>
    {
        config.Host(RabbitMqConsts.RabbitMqRootUri + $"{RabbitMqConsts.VirtualHost}", h =>
        {
            h.Username(RabbitMqConsts.UserName);
            h.Password(RabbitMqConsts.Password);
        });
        config.ReceiveEndpoint(SendToMailContractsQueue.NotificationQueueSendToMail, ep =>
        {
            ep.ConfigureConsumer<SendToMailConsumer>(ctx);

        });
        config.AutoStart = true;
    });
    x.AddRequestClient<SendToMailContract>();

});

builder.Services.AddTransient<ISendToMailService, SendToMailService>();

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

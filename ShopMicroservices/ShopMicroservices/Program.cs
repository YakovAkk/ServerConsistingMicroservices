using MassTransit;
using MicrocerviceContract.Queue;
using ShopMicroservices.httpClient.Base;
using ShopMicroservices.HttpWorker;
using ShopMicroservices.MassTransit;
using ShopMicroservices.RabbitMq;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<CategoryConsumer>();

    x.UsingRabbitMq((ctx, config) =>
    {
        config.Host(RabbitMqConsts.RabbitMqRootUri + $"{RabbitMqConsts.VirtualHost}", h =>
        {
            h.Username(RabbitMqConsts.UserName);
            h.Password(RabbitMqConsts.Password);
        });
        config.ReceiveEndpoint(ConstatsQueue.NotificationQueueNameCategories, ep =>
        {
            ep.ConfigureConsumer<CategoryConsumer>(ctx);
        });

        config.AutoStart = true;
    });

    //x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(config =>
    //{
    //    config.Host(RabbitMqConsts.RabbitMqRootUri + $"{RabbitMqConsts.VirtualHost}", h =>
    //    {
    //        h.Username(RabbitMqConsts.UserName);
    //        h.Password(RabbitMqConsts.Password);
    //    });
    //    config.ReceiveEndpoint(ConstatsQueue.NotificationQueueNameCategories, ep =>
    //    {
    //        ep.ConfigureConsumer<UserConsumer>(provider);
    //    });

    //    config.AutoStart = true;
    //}));
    
});
//builder.Services.AddMassTransitHostedService();


builder.Services.AddTransient<IHttpWorker, HttpWorker>();

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

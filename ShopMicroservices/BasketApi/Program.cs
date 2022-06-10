using BasketApi.RabbitMq;
using BasketBus.MassTransit.Consumers;
using BasketBus.MassTransit.Contracts;
using BasketBus.MassTransit.GlobalConsumers;
using BasketBus.MassTransit.Queues;
using BasketData.Data.Base.Models;
using BasketData.Data.DatabaseMongo;
using BasketRepository.RepositoriesMongo;
using BasketRepository.RepositoriesMongo.Base;
using BasketService.Services.Base;
using GlobalContracts.Queue;
using MassTransit;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<BasketCreateConsumer>();
    x.AddConsumer<BasketUpdateConsumer>();
    x.AddConsumer<BasketDeleteConsumer>();
    x.AddConsumer<IsBasketExistConsumer>();
    x.AddConsumer<DeleteFromBasketByIdConsumer>();
    x.AddConsumer<GetBasketItemByIdConsumer>();
    x.UsingRabbitMq((ctx, config) =>
    {
        config.Host(RabbitMqConsts.RabbitMqRootUri + $"{RabbitMqConsts.VirtualHost}", h =>
        {
            h.Username(RabbitMqConsts.UserName);
            h.Password(RabbitMqConsts.Password);
        });
        config.ReceiveEndpoint(BasketContractQueue.NotificationQueueNameBasket, ep =>
        {
            ep.ConfigureConsumer<BasketUpdateConsumer>(ctx);
            ep.ConfigureConsumer<BasketCreateConsumer>(ctx);
            ep.ConfigureConsumer<BasketDeleteConsumer>(ctx);
        });
        config.ReceiveEndpoint(GlobalQueues.NotificationQueueNameIsBasketExist, ep =>
        {
            ep.ConfigureConsumer<IsBasketExistConsumer>(ctx);
        });
        config.ReceiveEndpoint(GlobalQueues.NotificationQueueNameDeleteFromBasketById, ep =>
        {
            ep.ConfigureConsumer<DeleteFromBasketByIdConsumer>(ctx);
        });
        config.ReceiveEndpoint(GlobalQueues.NotificationQueueNameGetBasketItemById, ep =>
        {
            ep.ConfigureConsumer<GetBasketItemByIdConsumer>(ctx);
        });
        config.AutoStart = true;
    });
    x.AddRequestClient<BasketContractDelete>();
    x.AddRequestClient<BasketContractUpdate>();
    x.AddRequestClient<BasketContractCreate>();
});

builder.Services.AddTransient<IBasketService, BasketService.Services.BasketService>();
builder.Services.AddSingleton<MongoDatabase<BasketModel>>();

builder.Services.AddTransient<IBasketRepository, BasketRepositoty>();
builder.Services.Configure<BasketStoreDatabaseSettings>(builder.Configuration.GetSection("LegoStoreDatabase"));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
        {
            options.SlidingExpiration = true;
            options.ExpireTimeSpan = new TimeSpan(0, 1, 0);
        });

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

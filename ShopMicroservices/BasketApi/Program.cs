using BasketApi.RabbitMq;
using BasketBus.MassTransit.Consumers;
using BasketBus.MassTransit.Contracts;
using BasketBus.MassTransit.Queues;
using BasketData.Data.Base.Models;
using BasketData.Data.DatabaseMongo;
using BasketData.Data.DatabaseSql;
using BasketRepository.RepositoriesMongo;
using BasketRepository.RepositoriesMongo.Base;
using BasketRepository.RepositorySql;
using BasketRepository.RepositorySql.Base;
using BasketService.Services.Base;
using MassTransit;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<BasketCreateConsumer>();
    x.AddConsumer<BasketUpdateConsumer>();
    x.AddConsumer<BasketDeleteConsumer>();
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
        config.AutoStart = true;
    });
    x.AddRequestClient<BasketContractDelete>();
    x.AddRequestClient<BasketContractUpdate>();
    x.AddRequestClient<BasketContractCreate>();
});

builder.Services.AddTransient<IUserRepository, UserRepository>();

builder.Services.AddTransient<IBasketService, BasketService.Services.BasketService>();
builder.Services.AddSingleton<MongoDatabase<BasketModel>>();
builder.Services.AddDbContext<AppDBContent>(options =>
options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=LegoDB;Trusted_Connection=True;TrustServerCertificate=True;"));

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

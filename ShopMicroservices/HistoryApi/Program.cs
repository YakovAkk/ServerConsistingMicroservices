using GlobalContracts.Queue;
using HistoryApi.RabbitMq;
using HistoryBus.MassTransit.Consumers.LocalConsumers;
using HistoryBus.MassTransit.Contracts;
using HistoryBus.MassTransit.Queues;
using HistoryData.Data.DatabaseNoSql;
using HistoryData.Data.Models;
using HistoryRepository.RepositoriesMongo;
using HistoryRepository.RepositoriesMongo.Base;
using HistoryService.Services.Base;
using MassTransit;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<CreateHistoryConsumer>();
    x.AddConsumer<UpdateHistoryConsumer>();
    x.AddConsumer<DeleteHistoryConsumer>();
    x.UsingRabbitMq((ctx, config) =>
    {
        config.Host(RabbitMqConsts.RabbitMqRootUri + $"{RabbitMqConsts.VirtualHost}", h =>
        {
            h.Username(RabbitMqConsts.UserName);
            h.Password(RabbitMqConsts.Password);
        });
        config.ReceiveEndpoint(HistoryContractsQueue.NotificationQueueNameHistory, ep =>
        {
            ep.ConfigureConsumer<CreateHistoryConsumer>(ctx);
            ep.ConfigureConsumer<UpdateHistoryConsumer>(ctx);
            ep.ConfigureConsumer<DeleteHistoryConsumer>(ctx);
        });
        config.AutoStart = true;
    });
    x.AddRequestClient<HistoryContractDelete>();
    x.AddRequestClient<HistoryContractUpdate>();
    x.AddRequestClient<HistoryContractCreate>();
});
builder.Services.AddSingleton<MongoDatabase<HistoryModel>>();
builder.Services.AddTransient<IHistoryService, HistoryService.Services.HistoryService>();

builder.Services.AddTransient<IHistoryRepository, HistoryRepository.RepositoriesMongo.HistoryRepository>();
builder.Services.Configure<HistoryStoreDatabaseSettings>(builder.Configuration.GetSection("LegoStoreDatabase"));

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

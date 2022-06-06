using LegoData.Data.DatabaseMongo;
using LegoData.Data.Models;
using LegoRepository.RepositoriesMongo.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using MassTransit;
using LegoBus.MassTransit.Consumers;
using LegoBus.MassTransit.Contracts;
using LegoMicroservice.RabbitMq;
using LegoBus.MassTransit.Queues;
using LegoService.Services.Base;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<CreateLegoConsumer>();
    x.AddConsumer<UpdateLegoConsumer>();
    x.AddConsumer<DeleteLegoConsumer>();
    x.UsingRabbitMq((ctx, config) =>
    {
        config.Host(RabbitMqConsts.RabbitMqRootUri + $"{RabbitMqConsts.VirtualHost}", h =>
        {
            h.Username(RabbitMqConsts.UserName);
            h.Password(RabbitMqConsts.Password);
        });
        config.ReceiveEndpoint(LegoContractsQueue.NotificationQueueNameLego, ep =>
        {
            ep.ConfigureConsumer<CreateLegoConsumer>(ctx);
            ep.ConfigureConsumer<UpdateLegoConsumer>(ctx);
            ep.ConfigureConsumer<DeleteLegoConsumer>(ctx);
        });
        config.AutoStart = true;
    });
    x.AddRequestClient<LegoContractCreate>();
    x.AddRequestClient<LegoContractDelete>();
    x.AddRequestClient<LegoContractUpdate>();
});

builder.Services.AddMvcCore(config =>
{
    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
    config.Filters.Add(new AuthorizeFilter(policy));
});

builder.Services.AddTransient<ILegoRepository, LegoRepository.RepositoriesMongo.LegoRepository > ();
builder.Services.AddTransient<ILegoService, LegoService.Services.LegoService> ();

builder.Services.Configure<LegoStoreDatabaseSettings>(builder.Configuration.GetSection("LegoStoreDatabase"));

builder.Services.AddSingleton<MongoDatabase<LegoModel>>();

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

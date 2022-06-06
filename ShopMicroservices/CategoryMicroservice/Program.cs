using Bus.MassTransit.Consumers;
using Bus.MassTransit.Contracts.ContractsModel;
using Bus.MassTransit.Queues;
using CategoryData.Data.DatabaseNoSql;
using CategoryData.Data.Models;
using CategoryMicroservice.RabbitMq;
using CategoryRepositories.RepositoriesMongo;
using CategoryRepositories.RepositoriesMongo.Base;
using CategoryServices.Services;
using CategoryServices.Services.Base;
using MassTransit;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;




var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<CategoryCreateConsumer>();
    x.AddConsumer<CategoryUpdateConsumer>();
    x.AddConsumer<CategoryDeleteConsumer>();
    x.UsingRabbitMq((ctx, config) =>
    {
        config.Host(RabbitMqConsts.RabbitMqRootUri + $"{RabbitMqConsts.VirtualHost}", h =>
        {
            h.Username(RabbitMqConsts.UserName);
            h.Password(RabbitMqConsts.Password);
        });
        config.ReceiveEndpoint(CategoryContractsQueue.NotificationQueueNameCategories, ep =>
        {
            ep.ConfigureConsumer<CategoryUpdateConsumer>(ctx);
            ep.ConfigureConsumer<CategoryCreateConsumer>(ctx);
            ep.ConfigureConsumer<CategoryDeleteConsumer>(ctx);
        });
        config.AutoStart = true;
    });
    x.AddRequestClient<CategoryContractCreate>();
    x.AddRequestClient<CategoryContractUpdate>();
    x.AddRequestClient<CategoryContractDelete>();
});

builder.Services.AddMvcCore(config =>
{
    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
    config.Filters.Add(new AuthorizeFilter(policy));
});

builder.Services.AddTransient<ICategoryRepository, CategoryRepositoty>();
builder.Services.AddTransient<ICategoryService, CategoryService>();

builder.Services.Configure<LegoStoreDatabaseSettings>(builder.Configuration.GetSection("LegoStoreDatabase"));

builder.Services.AddSingleton<MongoDatabase<CategoryModel>>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddEndpointsApiExplorer();

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

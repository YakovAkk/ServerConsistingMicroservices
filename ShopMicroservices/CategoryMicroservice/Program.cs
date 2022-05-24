using CategoryData.Data.DatabaseNoSql;
using CategoryData.Data.Models;
using CategoryMicroservice.MassTransit;
using CategoryMicroservice.RabbitMq;
using CategoryRepositories.RepositoriesMongo;
using CategoryRepositories.RepositoriesMongo.Base;
using CategoryServices.Services;
using CategoryServices.Services.Base;
using MassTransit;
using MicrocerviceContract.Queue;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<UserConsumer>();
    x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(config =>
    {
        config.Host(RabbitMqConsts.RabbitMqUri + "/", h =>
        {
           
            h.Username(RabbitMqConsts.UserName);
            h.Password(RabbitMqConsts.Password);
        });
        config.ReceiveEndpoint(ConstatsQueue.NotificationQueueNameCategories, ep =>
        {
            ep.ConfigureConsumer<UserConsumer>(provider);
        });
    }));
});

builder.Services.AddMvcCore(config =>
{
    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
    config.Filters.Add(new AuthorizeFilter(policy));
});

builder.Services.AddTransient<ICategoryRepository, CategoryRepositoty>();
builder.Services.AddTransient<ICategoryService, CategoryService>();

builder.Services.Configure<LegoStoreDatabaseSettings>(
    builder.Configuration.GetSection("LegoStoreDatabase"));

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

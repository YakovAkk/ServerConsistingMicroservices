using AccountData.Database;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using AccountRepository.RepositorySql.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Identity;
using AccountService.Services.Interfaces;
using AccountService.Services;
using MassTransit;
using AccountMicroservice.RabbitMq;
using AccountBus.MassTransit.Queues;
using AccountBus.MassTransit.Consumers;
using AccountBus.MassTransit.Contracts;
using AccountBus.MassTransit.GlobalConsumers;
using GlobalContracts.Queue;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<DeleteConsumer>();
    x.AddConsumer<RegistrationConsumer>();
    x.AddConsumer<UpdateConsumer>();
    x.AddConsumer<IsUserExistConsumer>();
    x.UsingRabbitMq((ctx, config) =>
    {
        config.Host(RabbitMqConsts.RabbitMqRootUri + $"{RabbitMqConsts.VirtualHost}", h =>
        {
            h.Username(RabbitMqConsts.UserName);
            h.Password(RabbitMqConsts.Password);
        });
        config.ReceiveEndpoint(AccountContractQueue.NotificationQueueNameAccount, ep =>
        {
            ep.ConfigureConsumer<DeleteConsumer>(ctx);
            ep.ConfigureConsumer<RegistrationConsumer>(ctx);
            ep.ConfigureConsumer<UpdateConsumer>(ctx);
        });
        config.ReceiveEndpoint(GlobalQueues.NotificationQueueNameIsUserExist, ep =>
        {
            ep.ConfigureConsumer<IsUserExistConsumer>(ctx);
        });
        config.AutoStart = true;
    });
    x.AddRequestClient<AccountContractLogin>();
    x.AddRequestClient<AccountContractRegistration>();
    x.AddRequestClient<AccountContractUpdate>();
});


builder.Services.AddTransient<IAccountService, AccountService.Services.AccountService>();
builder.Services.AddTransient<IChangeAccountService, ChangeAccountService>();
builder.Services.AddTransient<ILoginAccountService, LoginAccountService>();

builder.Services.AddTransient<IAccountRepository, AccountRepository.RepositorySql.AccountRepository>();

builder.Services.AddDbContext<AppDBContent>(options =>
options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=LegoDB;Trusted_Connection=True;TrustServerCertificate=True;"));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
        {
            options.SlidingExpiration = true;
            options.ExpireTimeSpan = new TimeSpan(0, 1, 0);
        });

builder.Services.AddMvcCore(config =>
{
    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
    config.Filters.Add(new AuthorizeFilter(policy));
});


//Identity has been registered  here 
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppDBContent>();

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

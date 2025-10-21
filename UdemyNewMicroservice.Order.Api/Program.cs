#region

using Microsoft.EntityFrameworkCore;
using UdemyNewMicroservice.Bus;
using UdemyNewMicroservice.Order.Api.Endpoints.Orders;
using UdemyNewMicroservice.Order.Application;
using UdemyNewMicroservice.Order.Application.BackgroundServices;
using UdemyNewMicroservice.Order.Application.Contracts.Refit;
using UdemyNewMicroservice.Order.Application.Contracts.Repositories;
using UdemyNewMicroservice.Order.Application.Contracts.UnitOfWork;
using UdemyNewMicroservice.Order.Persistence;
using UdemyNewMicroservice.Order.Persistence.Repositories;
using UdemyNewMicroservice.Order.Persistence.UnitOfWork;
using UdemyNewMicroservice.Shared.Extensions;

#endregion

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCommonServiceExt(typeof(OrderApplicationAssembly));
builder.Services.AddCommonMasstransitExt(builder.Configuration);
builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});
builder.Services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));

builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddVersioningExt();
builder.Services.AddAuthenticationAndAuthorizationExt(builder.Configuration);
builder.Services.AddRefitConfigurationExt(builder.Configuration);

builder.Services.AddHostedService<CheckPaymentStatusOrderBackgroundService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    var dbContext = serviceProvider.GetRequiredService<AppDbContext>();
    await dbContext.Database.MigrateAsync();
}


app.AddOrderGroupEndpointExt(app.AddVersionSetExt());
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) app.MapOpenApi();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.Run();
using Microsoft.EntityFrameworkCore;
using UdemyNewMicroservice.Catalog.Api.Features.Categories;
using UdemyNewMicroservice.Order.Application;
using UdemyNewMicroservice.Order.Application.Contracts.Repositories;
using UdemyNewMicroservice.Order.Application.Contracts.UnitOfWork;
using UdemyNewMicroservice.Order.Persistence;
using UdemyNewMicroservice.Order.Persistence.Repositories;
using UdemyNewMicroservice.Order.Persistence.UnitOfWork;
using UdemyNewMicroservice.Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCommonServiceExt(typeof(OrderApplicationAssembly));
builder.Services.AddDbContext<AppDbContext>(option => { option.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")); });
builder.Services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));

builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddVersioningExt();
var app = builder.Build();
app.AddOrderGroupEndpointExt(app.AddVersionSetExt());
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) app.MapOpenApi();

app.UseHttpsRedirection();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.Run();
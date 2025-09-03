using Microsoft.EntityFrameworkCore;
using UdemyNewMicroservice.Bus;
using UdemyNewMicroservice.Payment.Api;
using UdemyNewMicroservice.Payment.Api.Feature.Payments;
using UdemyNewMicroservice.Payment.Api.Repositories;
using UdemyNewMicroservice.Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddVersioningExt();
builder.Services.AddCommonServiceExt(typeof(PaymentAssembly));
builder.Services.AddMasstransitExt(builder.Configuration);
builder.Services.AddDbContext<AppDbContext>(options => { options.UseInMemoryDatabase("payment-in-memory-db"); });


builder.Services.AddAuthenticationAndAuthorizationExt(builder.Configuration);


var app = builder.Build();
app.AddPaymentGroupEndpointExt(app.AddVersionSetExt());
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.UseAuthentication();
app.UseAuthorization();

app.Run();
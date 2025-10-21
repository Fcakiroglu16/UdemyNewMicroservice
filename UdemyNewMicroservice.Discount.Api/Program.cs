#region

using UdemyNewMicroservice.Discount.Api;
using UdemyNewMicroservice.Discount.Api.Features.Discounts;
using UdemyNewMicroservice.Discount.Api.Options;
using UdemyNewMicroservice.Discount.Api.Repositories;

#endregion

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOptionsExt();
builder.Services.AddDatabaseServiceExt();

builder.Services.AddCommonServiceExt(typeof(DiscountAssembly));
builder.Services.AddMasstransitExt(builder.Configuration);
builder.Services.AddVersioningExt();

builder.Services.AddAuthenticationAndAuthorizationExt(builder.Configuration);
var app = builder.Build();

app.MapDefaultEndpoints();
app.AddDiscountGroupEndpointExt(app.AddVersionSetExt());
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
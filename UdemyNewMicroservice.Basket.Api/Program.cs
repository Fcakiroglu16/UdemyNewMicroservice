#region

using UdemyNewMicroservice.Basket.Api;
using UdemyNewMicroservice.Basket.Api.Features.Baskets;
using UdemyNewMicroservice.Shared.Extensions;

#endregion

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCommonServiceExt(typeof(BasketAssembly));
builder.Services.AddMasstransitExt(builder.Configuration);
builder.Services.AddScoped<BasketService>();
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
});
builder.Services.AddVersioningExt();

builder.Services.AddAuthenticationAndAuthorizationExt(builder.Configuration);
var app = builder.Build();

app.UseExceptionHandler(x => { });

app.AddBasketGroupEndpointExt(app.AddVersionSetExt());
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();


app.Run();
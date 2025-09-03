using Microsoft.Extensions.FileProviders;
using UdemyNewMicroservice.Bus;
using UdemyNewMicroservice.Discount.Api;
using UdemyNewMicroservice.File.Api.Features.File;
using UdemyNewMicroservice.Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IFileProvider>(
    new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));

builder.Services.AddCommonServiceExt(typeof(FileAssembly));
builder.Services.AddMasstransitExt(builder.Configuration);
builder.Services.AddVersioningExt();

builder.Services.AddAuthenticationAndAuthorizationExt(builder.Configuration);
var app = builder.Build();
app.AddFileGroupEndpointExt(app.AddVersionSetExt());

app.UseStaticFiles();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.UseAuthentication();
app.UseAuthentication();
app.Run();
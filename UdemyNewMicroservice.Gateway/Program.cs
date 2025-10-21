#region

using UdemyNewMicroservice.Shared.Extensions;

#endregion

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddReverseProxy().LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));


builder.Services.AddAuthenticationAndAuthorizationExt(builder.Configuration);
var app = builder.Build();

app.MapDefaultEndpoints();
app.MapReverseProxy();
app.MapGet("/", () => "YARP (Gateway)");
app.UseAuthentication();
app.UseAuthorization();
app.Run();
#region

using Microsoft.AspNetCore.Authentication.Cookies;
using Refit;
using UdemyNewMicroservice.Web.DelegateHandlers;
using UdemyNewMicroservice.Web.Extensions;
using UdemyNewMicroservice.Web.Options;
using UdemyNewMicroservice.Web.Pages.Auth.SignIn;
using UdemyNewMicroservice.Web.Pages.Auth.SignUp;
using UdemyNewMicroservice.Web.Services;
using UdemyNewMicroservice.Web.Services.Refit;

#endregion

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddMvc(opt => opt.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);
builder.Services.AddOptionsExt();


builder.Services.AddHttpClient<SignUpService>();
builder.Services.AddHttpClient<SignInService>();
builder.Services.AddHttpClient<TokenService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<CatalogService>();
builder.Services.AddScoped<UserService>();

builder.Services.AddScoped<AuthenticatedHttpClientHandler>();
builder.Services.AddScoped<ClientAuthenticatedHttpClientHandler>();


builder.Services.AddRefitClient<ICatalogRefitService>().ConfigureHttpClient(configure =>
    {
        var microserviceOption = builder.Configuration.GetSection(nameof(MicroserviceOption)).Get<MicroserviceOption>();
        configure.BaseAddress = new Uri(microserviceOption!.Catalog.BaseAddress);
    }).AddHttpMessageHandler<AuthenticatedHttpClientHandler>()
    .AddHttpMessageHandler<ClientAuthenticatedHttpClientHandler>();


builder.Services.AddAuthentication(configureOption =>
    {
        configureOption.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        configureOption.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    })
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
    {
        options.LoginPath = "/Auth/SignIn";
        options.ExpireTimeSpan = TimeSpan.FromDays(60);
        options.Cookie.Name = "UdemyNewMicroserviceWebCookie";
        options.AccessDeniedPath = "/Auth/AccessDenied";
    });

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) app.UseExceptionHandler("/Error");

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
    .WithStaticAssets();

app.Run();
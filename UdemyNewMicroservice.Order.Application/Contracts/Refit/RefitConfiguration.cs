using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using UdemyNewMicroservice.Order.Application.Contracts.Refit.PaymentService;
using UdemyNewMicroservice.Shared.Options;

namespace UdemyNewMicroservice.Order.Application.Contracts.Refit
{
    public static class RefitConfiguration
    {
        public static IServiceCollection AddRefitConfigurationExt(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped<AuthenticatedHttpClientHandler>();

            services.AddRefitClient<IPaymentService>().ConfigureHttpClient(configure =>
            {
                var addressUrlOption = configuration.GetSection(nameof(AddressUrlOption)).Get<AddressUrlOption>();


                configure.BaseAddress = new Uri(addressUrlOption!.PaymentUrl);
            }).AddHttpMessageHandler<AuthenticatedHttpClientHandler>();


            return services;
        }
    }
}
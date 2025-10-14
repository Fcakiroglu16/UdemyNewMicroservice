#region

using MassTransit;
using UdemyNewMicroservice.Basket.Api.Consumers;
using UdemyNewMicroservice.Bus;

#endregion

namespace UdemyNewMicroservice.Basket.Api;

public static class MasstransitConfigurationExt
{
    public static IServiceCollection AddMasstransitExt(this IServiceCollection services,
        IConfiguration configuration)
    {
        var busOptions = configuration.GetSection(nameof(BusOption)).Get<BusOption>()!;


        services.AddMassTransit(configure =>
        {
            configure.AddConsumer<OrderCreatedEventConsumer>();


            configure.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host(new Uri($"rabbitmq://{busOptions.Address}:{busOptions.Port}"), host =>
                {
                    host.Username(busOptions.UserName);
                    host.Password(busOptions.Password);
                });

                cfg.ReceiveEndpoint("basket-microservice.order-created.queue",
                    e => { e.ConfigureConsumer<OrderCreatedEventConsumer>(ctx); });


                // cfg.ConfigureEndpoints(ctx);
            });
        });


        return services;
    }
}
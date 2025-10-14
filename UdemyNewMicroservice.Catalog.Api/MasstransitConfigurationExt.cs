#region

using UdemyNewMicroservice.Bus;
using UdemyNewMicroservice.Catalog.Api.Consumers;

#endregion

namespace UdemyNewMicroservice.Catalog.Api;

public static class MasstransitConfigurationExt
{
    public static IServiceCollection AddMasstransitExt(this IServiceCollection services,
        IConfiguration configuration)
    {
        var busOptions = configuration.GetSection(nameof(BusOption)).Get<BusOption>()!;


        services.AddMassTransit(configure =>
        {
            configure.AddConsumer<CoursePictureUploadedEventConsumer>();


            configure.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host(new Uri($"rabbitmq://{busOptions.Address}:{busOptions.Port}"), host =>
                {
                    host.Username(busOptions.UserName);
                    host.Password(busOptions.Password);
                });

                cfg.ReceiveEndpoint("catalog-microservice.course-picture-uploaded.queue",
                    e => { e.ConfigureConsumer<CoursePictureUploadedEventConsumer>(ctx); });


                // cfg.ConfigureEndpoints(ctx);
            });
        });


        return services;
    }
}
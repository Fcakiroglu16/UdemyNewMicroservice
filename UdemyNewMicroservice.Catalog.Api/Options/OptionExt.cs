#region

using Microsoft.Extensions.Options;

#endregion

namespace UdemyNewMicroservice.Catalog.Api.Options;

public static class OptionExt
{
    public static IServiceCollection AddOptionsExt(this IServiceCollection services)
    {
        services.AddOptions<MongoOption>().BindConfiguration(nameof(MongoOption)).ValidateDataAnnotations()
            .ValidateOnStart();


        services.AddSingleton(sp => sp.GetRequiredService<IOptions<MongoOption>>().Value);


        return services;
    }
}
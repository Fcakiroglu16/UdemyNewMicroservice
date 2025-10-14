#region

using MongoDB.Driver;
using UdemyNewMicroservice.Catalog.Api.Options;

#endregion

namespace UdemyNewMicroservice.Catalog.Api.Repositories;

public static class RepositoryExt
{
    public static IServiceCollection AddDatabaseServiceExt(this IServiceCollection services)
    {
        services.AddSingleton<IMongoClient, MongoClient>(sp =>
        {
            var options = sp.GetRequiredService<MongoOption>();
            return new MongoClient(options.ConnectionString);
        });

        services.AddScoped(sp =>
        {
            var mongoClient = sp.GetRequiredService<IMongoClient>();
            var options = sp.GetRequiredService<MongoOption>();

            return AppDbContext.Create(mongoClient.GetDatabase(options.DatabaseName));
        });


        return services;
    }
}
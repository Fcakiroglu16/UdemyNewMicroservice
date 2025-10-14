#region

using System.Reflection;
using MongoDB.Driver;
using UdemyNewMicroservice.Catalog.Api.Features.Categories;
using UdemyNewMicroservice.Catalog.Api.Features.Courses;

#endregion

namespace UdemyNewMicroservice.Catalog.Api.Repositories;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Course> Courses { get; set; }
    public DbSet<Category> Categories { get; set; }


    public static AppDbContext Create(IMongoDatabase database)
    {
        var optionsBuilder =
            new DbContextOptionsBuilder<AppDbContext>().UseMongoDB(database.Client,
                database.DatabaseNamespace.DatabaseName);


        return new AppDbContext(optionsBuilder.Options);
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
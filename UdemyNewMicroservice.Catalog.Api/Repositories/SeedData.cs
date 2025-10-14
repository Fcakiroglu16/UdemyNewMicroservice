#region

using UdemyNewMicroservice.Catalog.Api.Features.Categories;
using UdemyNewMicroservice.Catalog.Api.Features.Courses;

#endregion

namespace UdemyNewMicroservice.Catalog.Api.Repositories;

public static class SeedData
{
    public static async Task AddSeedDataExt(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        dbContext.Database.AutoTransactionBehavior = AutoTransactionBehavior.Never;

        if (!dbContext.Categories.Any())
        {
            var categories = new List<Category>
            {
                new() { Id = NewId.NextSequentialGuid(), Name = "Development" },
                new() { Id = NewId.NextSequentialGuid(), Name = "Business" },
                new() { Id = NewId.NextSequentialGuid(), Name = "IT & Software" },
                new() { Id = NewId.NextSequentialGuid(), Name = "Office Productivity" },
                new() { Id = NewId.NextSequentialGuid(), Name = "Personal Development" }
            };

            dbContext.Categories.AddRange(categories);
            await dbContext.SaveChangesAsync();
        }

        if (!dbContext.Courses.Any())
        {
            var category = await dbContext.Categories.FirstAsync();

            var randomUserId = NewId.NextGuid();

            List<Course> courses =
            [
                new()
                {
                    Id = NewId.NextSequentialGuid(),
                    Name = "C#",
                    Description = "C# Course",
                    Price = 100,
                    UserId = randomUserId,
                    Created = DateTime.UtcNow,
                    Feature = new Feature { Duration = 10, Rating = 4, EducatorFullName = "Ahmet Yıldız" },
                    CategoryId = category.Id
                },

                new()
                {
                    Id = NewId.NextSequentialGuid(),
                    Name = "Java",
                    Description = "Java Course",
                    Price = 200,
                    UserId = randomUserId,
                    Created = DateTime.UtcNow,
                    Feature = new Feature { Duration = 10, Rating = 4, EducatorFullName = "Ahmet Yıldız" },
                    CategoryId = category.Id
                },

                new()
                {
                    Id = NewId.NextSequentialGuid(),
                    Name = "Python",
                    Description = "Python Course",
                    Price = 300,
                    UserId = randomUserId,
                    Created = DateTime.UtcNow,
                    Feature = new Feature { Duration = 10, Rating = 4, EducatorFullName = "Ahmet Yıldız" },
                    CategoryId = category.Id
                }
            ];


            dbContext.Courses.AddRange(courses);
            await dbContext.SaveChangesAsync();
        }
    }
}
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using UdemyNewMicroservice.Web.Pages.Instructor.ViewModel;
using UdemyNewMicroservice.Web.Services.Refit;

namespace UdemyNewMicroservice.Web.Services
{
    public class CatalogService(ICatalogRefitService catalogRefitService, ILogger<CatalogService> logger)
    {


        public async Task<ServiceResult<List<CategoryViewModel>>> GetCategoriesAsync()
        {

            var response = await catalogRefitService.GetCategoriesAsync();
            if (!response.IsSuccessStatusCode)
            {
                var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(response.Error.Content!);
                logger.LogError("Error occurred while fetching categories");
                return ServiceResult<List<CategoryViewModel>>.Error("Fail to retrieve categories. Please try again later");
            }

            var categories = response!.Content!.Data!
                .Select(c => new CategoryViewModel(c.Id, c.Name))
                .ToList();
            return ServiceResult<List<CategoryViewModel>>.Success(categories);
        }
    }


}
}

#region

using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UdemyNewMicroservice.Web.Services;

#endregion

namespace UdemyNewMicroservice.Web.PageModels;

public class BasePageModel : PageModel
{
    public IActionResult ErrorPage(ServiceResult serviceResult, string? redirectToPage = null)
    {
        TempData["Error_Title"] = serviceResult.Fail!.Title;
        TempData["Error_Detail"] = serviceResult.Fail!.Detail;


        if (redirectToPage is not null) return RedirectToPage(redirectToPage);


        var validationError = serviceResult.Fail.Extensions.FirstOrDefault(x => x.Key == "errors");

        if (validationError.Value is null) return Page();

        var validationErrorAsDictionary = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(
            validationError.Value!
                .ToString()!);

        foreach (var fieldError in validationErrorAsDictionary!.SelectMany(fieldErrors => fieldErrors.Value))
            ModelState.AddModelError(string.Empty, fieldError);

        return Page();
    }

    public IActionResult SuccessPage(string message, string? redirectToPage = null)
    {
        TempData["Success"] = true;
        TempData["Success_Message"] = message;

        if (redirectToPage is not null) return RedirectToPage(redirectToPage);

        return Page();
    }
}
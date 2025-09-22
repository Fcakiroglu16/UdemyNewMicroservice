using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UdemyNewMicroservice.Web.Pages.Auth.SignUp;

namespace UdemyNewMicroservice.Web.Pages.Auth
{
    public class SignUpModel : PageModel
    {
        [BindProperty] public required SignUpViewModel SignUpViewModel { get; set; } = SignUpViewModel.Empty;


        public void OnGet()
        {
        }
    }
}
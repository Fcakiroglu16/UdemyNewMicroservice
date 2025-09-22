using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UdemyNewMicroservice.Web.Pages.Auth.SignUp;

namespace UdemyNewMicroservice.Web.Pages.Auth
{
    public class SignUpModel(SignUpService signUpService) : PageModel
    {
        [BindProperty] public required SignUpViewModel SignUpViewModel { get; set; } = SignUpViewModel.GetExampleModel;


        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            //validation
            var result = await signUpService.CreateAccount(SignUpViewModel);


            if (result.IsFail)
            {
                //show error
                return Page();
            }
            else
            {
                return RedirectToPage("/Index");
            }
        }
    }
}
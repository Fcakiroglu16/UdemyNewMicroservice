using System.ComponentModel.DataAnnotations;

namespace UdemyNewMicroservice.Web.Pages.Auth.SignUp
{
    public record SignUpViewModel(
        [Display(Name = "First Name:")] string FirstName,
        [Display(Name = "Last Name:")] string LastName,
        [Display(Name = "UserName :")] string UserName,
        [Display(Name = "Email :")] string Email,
        [Display(Name = "Password:")] string Password,
        [Display(Name = "Password Confirm:")] string PasswordConfirm)
    {
        public static SignUpViewModel Empty => new(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty,
            string.Empty);

        public static SignUpViewModel GetExampleModel => new("Ahmet", "Yıldız", "ahmetyildiz", "ahmet@outlook.com",
            "Password123.", "Password123.");
    }
}
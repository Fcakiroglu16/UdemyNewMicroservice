using System.ComponentModel.DataAnnotations;

namespace UdemyNewMicroservice.Web.Pages.Auth.SignUp
{
    public record SignUpViewModel(
        [Display(Name = "First Name:")]
        [Required(ErrorMessage = "First Name is required")]
        string FirstName,
        [Display(Name = "Last Name:")]
        [Required(ErrorMessage = "Last Name is required")]
        string LastName,
        [Display(Name = "UserName :")]
        [Required(ErrorMessage = "User Name is required")]
        string UserName,
        [Display(Name = "Email :")]
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        string Email,
        [Display(Name = "Password:")]
        [Required(ErrorMessage = "Password is required")]
        string Password,
        [Display(Name = "Password Confirm:")]
        [Required(ErrorMessage = "Password Confirm is required")]
        [Compare(nameof(Password), ErrorMessage = "The Password don't match")]
        string PasswordConfirm)
    {
        public static SignUpViewModel Empty => new(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty,
            string.Empty);

        public static SignUpViewModel GetExampleModel => new("Ahmet", "Yıldız", "ahmetyildiz", "ahmet@outlook.com",
            "Password123.", "Password123.");
    }
}
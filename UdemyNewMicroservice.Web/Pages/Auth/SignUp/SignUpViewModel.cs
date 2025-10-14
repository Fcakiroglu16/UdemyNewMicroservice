#region

using System.ComponentModel.DataAnnotations;

#endregion

namespace UdemyNewMicroservice.Web.Pages.Auth.SignUp;

public record SignUpViewModel
{
    [Display(Name = "First Name:")]
    [Required(ErrorMessage = "First Name is required")]
    public required string FirstName { get; init; }

    [Display(Name = "Last Name:")]
    [Required(ErrorMessage = "Last Name is required")]
    public required string LastName { get; init; }

    [Display(Name = "UserName :")]
    [Required(ErrorMessage = "User Name is required")]
    public required string UserName { get; init; }

    [Display(Name = "Email :")]
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    public required string Email { get; init; }

    [Display(Name = "Password:")]
    [Required(ErrorMessage = "Password is required")]
    public required string Password { get; init; }

    [Display(Name = "Password Confirm:")]
    [Required(ErrorMessage = "Password Confirm is required")]
    [Compare(nameof(Password), ErrorMessage = "The Password don't match")]
    public required string PasswordConfirm { get; init; }

    public static SignUpViewModel Empty => new()
    {
        FirstName = string.Empty,
        LastName = string.Empty,
        UserName = string.Empty,
        Email = string.Empty,
        Password = string.Empty,
        PasswordConfirm = string.Empty
    };

    public static SignUpViewModel GetExampleModel => new()
    {
        FirstName = "Ahmet",
        LastName = "Yıldız",
        UserName = "ahmetyildiz",
        Email = "ahmet@outlook.com",
        Password = "Password123.",
        PasswordConfirm = "Password123."
    };
}
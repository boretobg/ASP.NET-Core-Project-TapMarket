namespace TapMarket.Models.User
{
    using Microsoft.AspNetCore.Http;
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants.User;

    public class ManageFormModel
    {
        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength,
            ErrorMessage = "{0} must be between {2} and {1} characters.")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength,
            ErrorMessage = "{0} must be between {2} and {1} characters.")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6,
            ErrorMessage = "{0} must be at least {2} and at max {1} characters long.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(CityMaxLength, MinimumLength = CityMinLength,
            ErrorMessage = "{0} must be between {2} and {1} characters.")]
        public string City { get; set; }

        [Required]
        [StringLength(AddressMaxLength, MinimumLength = AddressMinLength,
            ErrorMessage = "{0} must be between {2} and {1} characters.")]
        public string Address { get; set; }

        [Required]
        public IFormFile ProfileImage { get; set; }
    }
}

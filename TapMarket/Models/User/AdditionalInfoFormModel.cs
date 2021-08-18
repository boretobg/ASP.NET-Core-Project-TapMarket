namespace TapMarket.Models.Customer
{
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Http;

    using static Data.DataConstants.User;

    public class AdditionalInfoFormModel
    {
        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength,
            ErrorMessage = "Username must be between {2} and {1} characters.")]
        public string Name { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(AddressMaxLength, MinimumLength = AddressMinLength,
            ErrorMessage = "Address must be between {2} and {1} characters.")]
        public string Address { get; set; }

        [Required]
        [StringLength(CityMaxLength, MinimumLength = CityMinLength,
            ErrorMessage = "City must be between {2} and {1} characters.")]
        public string City { get; set; }

        [Required]
        public IFormFile ProfileImage { get; set; }
    }
}

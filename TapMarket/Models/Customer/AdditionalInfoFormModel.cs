namespace TapMarket.Models.Customer
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants.Customer;

    public class AdditionalInfoFormModel
    {
        [Required]
        [StringLength(UsernameMaxLength, MinimumLength = UsernameMinLength,
            ErrorMessage = "Username must be between {2} and {1} characters.")]
        public string Username { get; set; }

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
        [Url]
        public string PictureUrl { get; set; }
    }
}

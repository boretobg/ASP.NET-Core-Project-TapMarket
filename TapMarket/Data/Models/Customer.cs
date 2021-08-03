namespace TapMarket.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.Customer;

    public class Customer
    {
        [Key]
        public int Id { get; init; } 

        [Required]
        [MaxLength(UsernameMaxLength)]
        public string Username { get; set; }

        [Required]
        [MaxLength(AddressMaxLength)]
        public string Address { get; set; }

        [Required]
        [MaxLength(CityMaxLength)]
        public string City { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [Url]
        public string PictureUrl { get; set; }

        [Required]
        public string UserId { get; set; }

        public IEnumerable<Listing> Listings { get; set; } = new List<Listing>();
        public IEnumerable<Message> Messages { get; set; } = new List<Message>();
    }
}

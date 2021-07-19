namespace TapMarket.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.Customer;

    public class Customer
    {
        [Required]
        public int Id { get; init; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(AddressMaxLength)]
        public string Address { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        public IEnumerable<Listing> Listings { get; set; } = new List<Listing>();
    }
}

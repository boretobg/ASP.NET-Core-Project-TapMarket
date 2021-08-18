namespace TapMarket.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.User;

    public class User : IdentityUser
    {
        [Required]
        [MaxLength(NameMaxLength)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(AddressMaxLength)]
        public string Address { get; set; }

        [Required]
        [MaxLength(CityMaxLength)]
        public string City { get; set; }

        public string ProfileImage { get; set; }

        public DateTime LastOnline { get; set; }

        public virtual IEnumerable<Listing> Listings { get; set; } = new List<Listing>();

        public virtual IEnumerable<Message> Messages { get; set; } = new List<Message>();

        public virtual IEnumerable<Favorite> Favorites { get; set; } = new List<Favorite>();
    }
}

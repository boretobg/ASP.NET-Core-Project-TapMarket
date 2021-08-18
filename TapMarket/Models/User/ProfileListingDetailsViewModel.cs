namespace TapMarket.Models.Customer
{
    using Microsoft.AspNetCore.Http;

    public class ProfileListingDetailsViewModel
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string ProfileImage { get; set; }

        public int ListingId { get; set; }
    }
}

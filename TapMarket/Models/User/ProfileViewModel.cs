namespace TapMarket.Models.Customer
{
    using System.Collections.Generic;
    using TapMarket.Models.Listing;

    public class ProfileViewModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string PhoneNumber { get; set; }

        public string ProfileImage { get; set; }

        public IEnumerable<ListingViewModel> Listings { get; set; } = new List<ListingViewModel>();
    }
}

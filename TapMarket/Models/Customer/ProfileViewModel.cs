using System.Collections.Generic;
using TapMarket.Models.Listing;

namespace TapMarket.Models.Customer
{
    public class ProfileViewModel
    {
        public string Username { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string PhoneNumber { get; set; }

        public string PictureUrl { get; set; }

        public IEnumerable<ListingViewModel> Listings { get; set; } = new List<ListingViewModel>();
    }
}

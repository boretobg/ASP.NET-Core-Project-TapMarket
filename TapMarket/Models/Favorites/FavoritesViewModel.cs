namespace TapMarket.Models.Favorites
{
    using System.Collections.Generic;
    using TapMarket.Models.Listing;

    public class FavoritesViewModel
    {
        public string CustomerId { get; set; }
        public IEnumerable<ListingViewModel> Listings { get; set; } = new List<ListingViewModel>();
    }
}

namespace TapMarket.Services
{
    using System.Collections.Generic;
    using TapMarket.Models.Listing;

    public interface IListingService
    {
        public ICollection<ListingViewModel> GetListings();
    }
}

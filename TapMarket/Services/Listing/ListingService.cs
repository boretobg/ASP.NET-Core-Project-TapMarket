namespace TapMarket.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using TapMarket.Data;
    using TapMarket.Models.Listing;

    public class ListingService : IListingService
    {
        private readonly TapMarketDbContext data;

        public ListingService(TapMarketDbContext data) 
            => this.data = data;

        public ICollection<ListingViewModel> GetListings(string userId)
        {
            var listingsQuery = this.data.Listings.AsQueryable();

            var listings = listingsQuery
                .Where(c => c.User.Id == userId)
                .Select(l => new ListingViewModel
                {
                    Id = l.Id,
                    Title = l.Title,
                    Price = l.Price,
                    Condition = l.Condition.Name,
                    ListingImage = l.ListingImage
                }).ToList();

            return listings;
        }
    }
}

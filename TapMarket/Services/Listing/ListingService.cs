namespace TapMarket.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using TapMarket.Data;
    using TapMarket.Models.Listing;

    public class ListingService : IListingService
    {
        private TapMarketDbContext data;

        public ListingService(TapMarketDbContext data) 
            => this.data = data;

        public ICollection<ListingViewModel> GetListings(string userId)
        {
            var listingsQuery = this.data.Listings.AsQueryable();

            var listings = listingsQuery
                .Where(c => c.Customer.Id == userId)
                .Select(l => new ListingViewModel
                {
                    Id = l.Id,
                    Title = l.Title,
                    Price = l.Price,
                    Condition = l.Condition.Name,
                    ImageUrl = l.ImageUrl
                }).ToList();

            return listings;
        }
    }
}

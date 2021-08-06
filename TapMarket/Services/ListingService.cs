namespace TapMarket.Services
{
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TapMarket.Data;
    using TapMarket.Infrastructure;
    using TapMarket.Models.Listing;

    public class ListingService : Controller, IListingService
    {
        private TapMarketDbContext data;

        public ListingService(TapMarketDbContext data) 
            => this.data = data;

        public ICollection<ListingViewModel> GetListings()
        {
            var listingsQuery = this.data.Listings.AsQueryable();

            var userId = this.User.GetId();

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

namespace TapMarket.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;
    using TapMarket.Data;
    using TapMarket.Data.Models;
    using TapMarket.Infrastructure;
    using TapMarket.Models.Listing;
    using TapMarket.Services;

    public class FavoritesController : Controller
    {
        private readonly TapMarketDbContext data;
        private readonly IListingService listingService;

        public FavoritesController(TapMarketDbContext data, IListingService listingService)
        {
            this.data = data;
            this.listingService = listingService;
        }


        [Authorize]
        public IActionResult All()
        {
            var favorites = this.data
                .Favorites
                .Where(f => f.UserId == this.User.GetId())
                .ToList();

            var listings = new List<ListingViewModel>();

            foreach (var favorite in favorites)
            {
                var tempListing = this.data
                    .Listings
                    .Where(x => x.Id == favorite.ListingId)
                    .Select(l => new ListingViewModel
                    { 
                        Id = l.Id,
                        Title = l.Title,
                        ImageUrl = l.ImageUrl,
                        Condition = l.Condition.Name,
                        Price = l.Price
                    })
                    .FirstOrDefault();

                if (tempListing != null)
                {
                    listings.Add(tempListing);
                }
            }

            return View(listings);
        }
    }
}

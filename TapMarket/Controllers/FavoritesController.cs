namespace TapMarket.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using TapMarket.Data;
    using TapMarket.Infrastructure;
    using TapMarket.Models.Listing;
    using TapMarket.Services;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class FavoritesController : Controller
    {
        private readonly TapMarketDbContext data;

        public FavoritesController(TapMarketDbContext data) 
            => this.data = data;

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
                        ListingImage = l.ListingImage,
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

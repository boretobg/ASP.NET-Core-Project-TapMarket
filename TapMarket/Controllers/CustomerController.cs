namespace TapMarket.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using TapMarket.Data;
    using TapMarket.Models.Listing;

    public class CustomerController : Controller
    {
        private readonly TapMarketDbContext data;

        public CustomerController(TapMarketDbContext data) 
            => this.data = data;

        public IActionResult Profile()
        {
            var listingsQuery = this.data.Listings.AsQueryable();

            var listings = listingsQuery
                .Select(l => new ListingViewModel
                { 
                    Title = l.Title,
                    Price = l.Price,
                    Condition = l.Condition,
                    ImageUrl = l.ImageUrl
                }).ToList();

            return View(listings);
        }
    }
}

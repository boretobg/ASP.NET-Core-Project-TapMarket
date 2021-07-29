namespace TapMarket.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Diagnostics;
    using System.Linq;
    using TapMarket.Data;
    using TapMarket.Infrastructure;
    using TapMarket.Models;
    using TapMarket.Models.Listing;

    public class HomeController : Controller
    {
        private readonly TapMarketDbContext data;
        public HomeController(TapMarketDbContext data)
        { 
            this.data = data;
        }

        public IActionResult Index()
        {
            var listingsQuery = this.data.Listings.AsQueryable();

            var listings = listingsQuery
                .Select(l => new ListingViewModel
                {
                    Id = l.Id,
                    Title = l.Title,
                    Price = l.Price,
                    Condition = l.Condition.Name,
                    ImageUrl = l.ImageUrl
                }).ToList();

            var rnd = new Random();
            var shuffledListings = listings.OrderBy(c => rnd.Next()).ToList();

            ViewBag.ShuffledListings = shuffledListings;
            ViewBag.Customer = this.data.Customers.Where(c => c.UserId == this.User.GetId()).FirstOrDefault();

            return View();
        }

        public IActionResult About() 
            => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

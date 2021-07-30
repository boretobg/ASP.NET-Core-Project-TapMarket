namespace TapMarket.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using TapMarket.Data;
    using TapMarket.Data.Models;
    using TapMarket.Infrastructure;
    using TapMarket.Models;
    using TapMarket.Models.Home;
    using TapMarket.Models.Listing;

    public class HomeController : Controller
    {
        private readonly TapMarketDbContext data;
        public HomeController(TapMarketDbContext data)
        { 
            this.data = data;
        }

        [HttpPost]
        public IActionResult Index(HomeFormModel homeInfo)
        {
            if (string.IsNullOrEmpty(homeInfo.SearchInput) || string.IsNullOrWhiteSpace(homeInfo.SearchInput))
            {
                var listings = this.data
                    .Listings
                    .Where(l => l.Title.Contains(homeInfo.SearchInput))
                    .ToList();
            }

            return View();
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

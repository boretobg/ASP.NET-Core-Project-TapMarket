using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TapMarket.Data;
using TapMarket.Models;
using TapMarket.Models.Listing;

namespace TapMarket.Controllers
{
    public class HomeController : Controller
    {
        private readonly TapMarketDbContext data;

        public HomeController(TapMarketDbContext data) 
            => this.data = data;

        public IActionResult Index()
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

        public IActionResult About() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

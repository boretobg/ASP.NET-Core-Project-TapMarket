namespace TapMarket.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Diagnostics;
    using System.Linq;
    using TapMarket.Data;
    using TapMarket.Models;
    using TapMarket.Models.Listing;
    using TapMarket.Services;

    public class HomeController : Controller
    {
        private readonly TapMarketDbContext data;
        private readonly IUserService user;

        public HomeController(TapMarketDbContext data, IUserService user)
        { 
            this.data = data;
            this.user = user;
        }

        public IActionResult Index()
        {
            var listingsQuery = this.data.Listings.AsQueryable();

            var listings = listingsQuery
                .Select(l => new ListingViewModel
                {
                    Title = l.Title,
                    Price = l.Price,
                    ConditionId = l.ConditionId,
                    ImageUrl = l.ImageUrl
                }).ToList();

            ViewBag.Listings = listings;
            ViewBag.Customer = this.user.GetId();

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

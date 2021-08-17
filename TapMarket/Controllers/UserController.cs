namespace TapMarket.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using TapMarket.Data;
    using TapMarket.Infrastructure;
    using TapMarket.Models.Customer;
    using TapMarket.Services;

    public class UserController : Controller
    {
        private readonly TapMarketDbContext data;
        private readonly IListingService listingService;

        public UserController(TapMarketDbContext data, IListingService listingService)
        {
            this.data = data;
            this.listingService = listingService;
        }

        [Authorize]
        public IActionResult Listings()
        {
            var listings = this.listingService.GetListings(this.User.GetId());

            return View(listings);
        }
        
        [Authorize]
        public IActionResult Profile()
        {
            var listings = this.listingService.GetListings(this.User.GetId());

            var customer = this.data
                .User
                .Where(c => c.Id == this.User.GetId())
                .Select(c => new ProfileViewModel
                { 
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    Address = c.Address,
                    City = c.City,
                    PhoneNumber = c.PhoneNumber,
                    PictureUrl = c.PictureUrl,
                    Listings = listings
                })
                .FirstOrDefault();

            ViewBag.Customer = customer;
            ViewBag.Listings = listings;

            return View();
        }
    }
}


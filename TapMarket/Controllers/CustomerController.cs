namespace TapMarket.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;
    using TapMarket.Data;
    using TapMarket.Infrastructure;
    using TapMarket.Models.Customer;
    using TapMarket.Models.Listing;
    using TapMarket.Services;

    public class CustomerController : Controller
    {
        private readonly TapMarketDbContext data;
        private readonly IListingService listingService;

        public CustomerController(TapMarketDbContext data, IListingService listingService)
        {
            this.data = data;
            this.listingService = listingService;
        }

        public IActionResult Listings()
        {
            var listings = this.listingService.GetListings();

            return View(listings);
        }

        public IActionResult Profile()
        {
            var listings = this.listingService.GetListings();

            var customer = this.data
                .Customers
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


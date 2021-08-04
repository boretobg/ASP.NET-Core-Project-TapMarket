namespace TapMarket.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using TapMarket.Data;
    using TapMarket.Infrastructure;
    using TapMarket.Models.Customer;
    using TapMarket.Models.Listing;

    public class CustomerController : Controller
    {
        private readonly TapMarketDbContext data;

        public CustomerController(TapMarketDbContext data)
        {
            this.data = data;
        }

        public IActionResult Profile()
        {
            var listingsQuery = this.data.Listings.AsQueryable();

            var listings = listingsQuery
                .Where(c => c.Customer.Id == this.User.GetId())
                .Select(l => new ListingViewModel
                {
                    Id = l.Id,
                    Title = l.Title,
                    Price = l.Price,
                    Condition = l.Condition.Name,
                    ImageUrl = l.ImageUrl
                }).ToList();

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


namespace TapMarket.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.IO;
    using System.Linq;
    using TapMarket.Data;
    using TapMarket.Data.Models;
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

        [Authorize]
        public IActionResult Additional()
            => View();

        [HttpPost]
        [Authorize]
        public IActionResult Additional(AdditionalInfoFormModel customerInfo)
        {
            if (!ModelState.IsValid)
            {
                return View(customerInfo);
            }


            var customer = new Customer
            {
                Username = customerInfo.Username,
                PhoneNumber = customerInfo.PhoneNumber,
                Address = customerInfo.Address,
                City = customerInfo.City,
                PictureUrl = customerInfo.PictureUrl,
                UserId = this.User.GetId()
            };

            this.data.Customers.Add(customer);
            this.data.SaveChanges();

            return Redirect("/Listing/Add");
        }

        public IActionResult Profile()
        {
            var listingsQuery = this.data.Listings.AsQueryable();

            var listings = listingsQuery
                .Where(c => c.Customer.UserId == this.User.GetId())
                .Select(l => new ListingViewModel
                {
                    Title = l.Title,
                    Price = l.Price,
                    Condition = l.Condition.Name,
                    ImageUrl = l.ImageUrl
                }).ToList();

            var customer = this.data
                .Customers
                .Where(c => c.UserId == this.User.GetId())
                .Select(c => new ProfileViewModel
                { 
                    Username = c.Username,
                    Address = c.Address,
                    City = c.City,
                    PhoneNumber = c.PhoneNumber,
                    PictureUrl = c.PictureUrl,
                    Listings = listings
                });

            ViewBag.Customer = customer;

            return View();
        }
    }
}


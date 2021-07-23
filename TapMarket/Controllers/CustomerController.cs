﻿namespace TapMarket.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using System.Security.Claims;
    using TapMarket.Data;
    using TapMarket.Data.Models;
    using TapMarket.Models.Customer;
    using TapMarket.Models.Listing;

    public class CustomerController : Controller
    {
        private readonly TapMarketDbContext data;

        public CustomerController(TapMarketDbContext data) 
            => this.data = data;

        public IActionResult Additional() 
            => View();

        [HttpPost]
        public IActionResult Additional(AdditionalInfoFormModel customerInfo)
        {
            if (!ModelState.IsValid)
            {
                return View(customerInfo);
            }

            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var customer = new Customer
            {
                Username = customerInfo.Username,
                PhoneNumber = customerInfo.PhoneNumber,
                Address = customerInfo.Address,
                City = customerInfo.City,
                UserId = userId
            };

            this.data.Customers.Add(customer);
            this.data.SaveChanges();

            return Redirect("/Listing/Add");
        }

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

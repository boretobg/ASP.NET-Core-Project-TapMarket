namespace TapMarket.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TapMarket.Data;
    using TapMarket.Data.Models;
    using TapMarket.Infrastructure;
    using TapMarket.Models.Customer;
    using TapMarket.Models.Listing;

    public class ListingController : Controller
    {
        private readonly TapMarketDbContext data;

        public ListingController(TapMarketDbContext data)
        {
            this.data = data;
        }

        public IActionResult Delete(int listingId)
        {
            var listing = this.data.Listings.Where(x => x.Id == listingId).FirstOrDefault();

            if (listing != null)
            {
                this.data.Listings.Remove(listing);
            }

            this.data.SaveChanges();

            return Redirect("/Customer/Profile");
        }

        [Authorize]
        public IActionResult Details(int listingId)
        {
            var listing = this.data
                .Listings.Where(x => x.Id == listingId)
                .Select(l => new ListingDetailsViewModel
                {
                    Id = l.Id,
                    Title = l.Title,
                    Price = l.Price,
                    ImageUrl = l.ImageUrl,
                    Description = l.Description,
                    Category = l.Category.Name,
                    Condition = l.Condition.Name,
                    CreatedOn = l.CreatedOn
                }).FirstOrDefault();

            var customer = this.data
                .Customers
                .Where(c => c.Listings.Any(l => l.Id == listing.Id))
                .Select(c => new ProfileListingDetailsViewModel
                {
                    Username = c.Username,
                    Address = c.Address,
                    City = c.City,
                    PhoneNumber = c.PhoneNumber,
                    PictureUrl = c.PictureUrl,
                    ListingId = listing.Id
                }).FirstOrDefault();


            //this might not work in some cases?? idk
            if (listing == null || customer == null)
            {
                return Redirect("/Home/Index");
            }

            ViewBag.Listing = listing;
            ViewBag.Customer = customer;

            return View();
        }

        [Authorize]
        public IActionResult Add()
        {
            if (!this.data.Customers.Any(c => c.UserId == this.User.GetId()))
            {
                return Redirect("/Customer/Additional");
            }

            return View(new AddListingFormModel
            {
                Categories = this.GetCategories(),
                Conditions = this.GetConditions()
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(AddListingFormModel listing)
        {
            if (!this.data.Categories.Any(c => c.Id == listing.CategoryId))
            {
                this.ModelState.AddModelError(nameof(listing.CategoryId), "Category does not exist.");
            }
            if (!this.data.Conditions.Any(c => c.Id == listing.ConditionId))
            {
                this.ModelState.AddModelError(nameof(listing.ConditionId), "Condition does not exist.");
            }


            if (!ModelState.IsValid)
            {
                listing.Categories = this.GetCategories();
                listing.Conditions = this.GetConditions();

                return View(listing);
            }


            var customerId = this
                .data
                .Customers
                .Where(c => c.UserId == this.User.GetId())
                .Select(c => c.Id)
                .FirstOrDefault();

            var listingData = new Listing
            {
                Id = listing.Id,
                Title = listing.Title,
                ImageUrl = listing.ImageUrl,
                Description = listing.Description,
                CategoryId = listing.CategoryId,
                Price = listing.Price,
                ConditionId = listing.ConditionId,
                CreatedOn = DateTime.UtcNow,
                CustomerId = customerId
            };

            this.data.Listings.Add(listingData);
            this.data.SaveChanges();

            return Redirect("/Listing/CreatedListing");
        }

        [Authorize]
        public IActionResult CreatedListing()
            => View();

        private IEnumerable<ListingCategoryViewModel> GetCategories()
         => this.data
            .Categories
            .Select(c => new ListingCategoryViewModel
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();

        private IEnumerable<ListingConditionViewModel> GetConditions()
            => this.data
             .Conditions
            .Select(c => new ListingConditionViewModel
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();
    }
}

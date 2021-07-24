namespace TapMarket.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using TapMarket.Data;
    using TapMarket.Data.Models;
    using TapMarket.Models.Listing;

    public class ListingController : Controller
    {
        private readonly TapMarketDbContext data;

        public ListingController(TapMarketDbContext data)
            => this.data = data;

        [Authorize]
        public IActionResult Add()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (!this.data.Customers.Any(c => c.UserId == userId))
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

            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var customerId = this
                .data
                .Customers
                .Where(c => c.UserId == userId)
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
        {
            return View();
        }


        private IEnumerable<ListingCategoryViewModel> GetCategories()
         => this.data
            .Categories
            .Select(c => new ListingCategoryViewModel
            {
                Id = c.Id,
                Name = c.Name
            })
            .ToList();

        private IEnumerable<ListingConditionViewModel> GetConditions()
            => this.data
             .Conditions
            .Select(c => new ListingConditionViewModel
            {
                Id = c.Id,
                Name = c.Name
            })
            .ToList();
    }
}

namespace TapMarket.Controllers
{
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

        public IActionResult Add()
            => View(new AddListingFormModel
            {
                Categories = this.GetCategories()
            });

        [HttpPost]
        public IActionResult Add(AddListingFormModel listing)
        {
            if (!this.data.Categories.Any(c => c.Id == listing.CategoryId))
            {
                this.ModelState.AddModelError(nameof(listing.CategoryId), "Category does not exist.");
            }

            if (!ModelState.IsValid)
            {
                listing.Categories = this.GetCategories();

                return View(listing);
            }

            var listingData = new Listing
            {
                Id = listing.Id,
                Title = listing.Title,
                ImageUrl = listing.ImageUrl,
                Description = listing.Description,
                CategoryId = listing.CategoryId,
                Price = listing.Price,
                Condition = listing.Condition,
                CreatedOn = DateTime.UtcNow
            };

            this.data.Listings.Add(listingData);
            this.data.SaveChanges();

            return Redirect("/Listing/CreatedListing");
        }

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
    }
}

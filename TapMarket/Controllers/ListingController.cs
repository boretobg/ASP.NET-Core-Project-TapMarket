﻿namespace TapMarket.Controllers
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
            => this.data = data;

        [Authorize]
        public IActionResult Delete(int listingId)
        {
            var listing = this.data.Listings.Where(x => x.Id == listingId).FirstOrDefault();

            if (listing != null)
            {
                this.data.Listings.Remove(listing);
            }

            this.data.SaveChanges();

            return Redirect("/User/Profile");
        }

        [Authorize]
        [HttpPost]
        public IActionResult Details(ListingDetailFormModel info)
        {
            if (info.Command == "Add to favorites")
            {
                var favorite = this.data
                    .Favorites
                    .Where(f => f.CustomerId == info.CustomerId && f.ListingId == info.ListingId)
                    .FirstOrDefault();

                if (favorite != null)
                {
                    this.data.Favorites.Remove(favorite);

                }
            }
            else if (info.Command == "Remove from favorites")
            {
                var favorite = new Favorite
                {
                    CustomerId = info.CustomerId,
                    ListingId = info.ListingId,
                };

                this.data.Favorites.Add(favorite);
            }

            this.data.SaveChanges();

            return Redirect($"/Listing/Details?listingId={info.ListingId}");
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
                    CreatedOn = l.CreatedOn,
                }).FirstOrDefault();

            var customer = this.data
                .User
                .Where(c => c.Listings.Any(l => l.Id == listing.Id))
                .Select(c => new ProfileListingDetailsViewModel
                {
                    Address = c.Address,
                    City = c.City,
                    PhoneNumber = c.PhoneNumber,
                    PictureUrl = c.PictureUrl,
                    ListingId = listing.Id,
                    Email = c.Email
                }).FirstOrDefault();


            if (listing == null || customer == null)
            {
                return Redirect("/Home/Index");
            }

            var isFavorite = false;
            var favorite = this.data
                .Favorites
                .Where(x => x.CustomerId == this.User.GetId() && x.ListingId == listing.Id)
                .FirstOrDefault();

            if (favorite != null)
            {
                isFavorite = true;
            }
            
            ViewBag.Listing = listing;
            ViewBag.IsFavorite = isFavorite;
            ViewBag.Customer = customer;
            ViewBag.UserId = this.User.GetId();

            return View();
        }

        [Authorize]
        public IActionResult Add()
        {
            if (!this.data.User.Any(c => c.Id == this.User.GetId()))
            {
                return Redirect("/User/Additional");
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
                .User
                .Where(c => c.Id == this.User.GetId())
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
                UserId = customerId
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

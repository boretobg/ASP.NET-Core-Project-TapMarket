namespace TapMarket.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TapMarket.Data;
    using TapMarket.Data.Models;
    using TapMarket.Infrastructure;
    using TapMarket.Models.Customer;
    using TapMarket.Models.Listing;
    using TapMarket.Services;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;

    public class ListingController : Controller
    {
        private readonly TapMarketDbContext data;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IFileService fileService;

        public ListingController(
            TapMarketDbContext data,
            IWebHostEnvironment webHostEnvironment,
            IFileService fileService)
        {
            this.data = data;
            this.webHostEnvironment = webHostEnvironment;
            this.fileService = fileService;
        }

        [Authorize]
        public IActionResult Edit(int listingId)
        {
            var listing = this.data
                .Listings
                .Where(l => l.Id == listingId)
                .FirstOrDefault();

            if (listing == null)
            {
                return Redirect("/Home/Index");
            }

            return View(new AddListingFormModel
            {
                Id = listing.Id,
                Price = listing.Price,
                Description = listing.Description,
                Title = listing.Title,
                CategoryId = listing.CategoryId,
                ConditionId = listing.ConditionId,
                Categories = this.GetCategories(),
                Conditions = this.GetConditions(),
                }) ;
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(AddListingFormModel listing)
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

            this.data.Listings.Remove(this.data
               .Listings
               .Where(l => l.Id == listing.TempListingId)
               .FirstOrDefault());

            var editedListing = new Listing
            {
                Id = listing.Id,
                Title = listing.Title,
                ListingImage = this.fileService.UploadFile(listing.ListingImage),
                Description = listing.Description,
                CategoryId = listing.CategoryId,
                Price = listing.Price,
                ConditionId = listing.ConditionId,
                CreatedOn = DateTime.Now,
                UserId = this.User.GetId()
            };

            this.data.Listings.Add(editedListing);
            this.data.SaveChanges();

            return Redirect($"/Listing/Details?listingId={editedListing.Id}");
        }

        public IActionResult ModeratorDelete(int listingId)
        {
            DeleteListing(listingId);
            return Redirect("/Home/Index");
        }

        [Authorize]
        public IActionResult Delete(int listingId)
        {
            DeleteListing(listingId);
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
                    .Where(f => f.UserId == info.UserId && f.ListingId == info.ListingId)
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
                    UserId = info.UserId,
                    ListingId = info.ListingId,
                };

                this.data.Favorites.Add(favorite);
            }

            if (info.Message != null)
            {
                var message = this.data
                    .Messages
                    .Where(x => (x.ReceiverId == info.ReceiverId && x.SenderId == info.UserId)
                            || x.ReceiverId == info.UserId && x.SenderId == info.ReceiverId)
                    .FirstOrDefault();

                if (message == null)
                {
                    message = new Message
                    {
                        Receiver = this.data.User.Where(x => x.Id == info.ReceiverId).FirstOrDefault(),
                        ReceiverId = info.ReceiverId,
                        Sender = this.data.User.Where(x => x.Id == info.UserId).FirstOrDefault(),
                        SenderId = info.UserId
                    };
                }

                var messageContent = new MessageContent
                {
                    Text = info.Message,
                    SentOn = DateTime.Now,
                    SenderId = info.UserId
                };

                message.Content.Add(messageContent);

                this.data.MessageContents.Add(messageContent);
                this.data.Messages.Attach(message);
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
                    ListingImage = l.ListingImage,
                    Description = l.Description,
                    Category = l.Category.Name,
                    Condition = l.Condition.Name,
                    CreatedOn = l.CreatedOn,
                }).FirstOrDefault();

            var user = this.data
                .User
                .Where(c => c.Listings.Any(l => l.Id == listing.Id))
                .Select(c => new ProfileListingDetailsViewModel
                {
                    Id = c.Id,
                    Username = $"{c.FirstName} {c.LastName}",
                    Address = c.Address,
                    City = c.City,
                    PhoneNumber = c.PhoneNumber,
                    ListingId = listing.Id,
                    Email = c.Email,
                    ProfileImage = c.ProfileImage
                }).FirstOrDefault();


            if (listing == null || user == null)
            {
                return Redirect("/Home/Index");
            }

            var isFavorite = false;
            var favorite = this.data
                .Favorites
                .Where(x => x.UserId == this.User.GetId() && x.ListingId == listing.Id)
                .FirstOrDefault();

            if (favorite != null)
            {
                isFavorite = true;
            }

            ViewBag.Listing = listing;
            ViewBag.IsFavorite = isFavorite;
            ViewBag.User = user;
            ViewBag.CurrentUserId = this.User.GetId();

            return View();
        }

        [Authorize]
        public IActionResult Add()
        {
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

            var listingData = new Listing
            {
                Id = listing.Id,
                Title = listing.Title,
                ListingImage = this.fileService.UploadFile(listing.ListingImage),
                Description = listing.Description,
                CategoryId = listing.CategoryId,
                Price = listing.Price,
                ConditionId = listing.ConditionId,
                CreatedOn = DateTime.Now,
                UserId = GetUserId()
            };

            this.data.Listings.Add(listingData);
            this.data.SaveChanges();

            return Redirect("/Listing/CreatedListing");
        }

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

        private string GetUserId()
            => this.data
                .User
                .Where(c => c.Id == this.User.GetId())
                .Select(c => c.Id)
                .FirstOrDefault();

        private void DeleteListing(int listingId)
        {
            var listing = this.data.Listings.Where(x => x.Id == listingId).FirstOrDefault();

            if (listing != null)
            {
                this.data.Listings.Remove(listing);
            }

            this.data.SaveChanges();
        }
    }
}

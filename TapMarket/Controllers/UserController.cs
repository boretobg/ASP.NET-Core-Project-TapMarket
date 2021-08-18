namespace TapMarket.Controllers
{
    using System.Linq;
    using TapMarket.Data;
    using TapMarket.Infrastructure;
    using TapMarket.Models.Customer;
    using TapMarket.Services;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using TapMarket.Models.User;
    using TapMarket.Data.Models;
    using System;

    public class UserController : Controller
    {
        private readonly TapMarketDbContext data;
        private readonly IListingService listingService;
        private readonly IFileService fileService;

        public UserController(
            TapMarketDbContext data, 
            IListingService listingService, 
            IFileService fileService)
        {
            this.data = data;
            this.listingService = listingService;
            this.fileService = fileService;
        }

        [Authorize]
        public IActionResult Manage()
        {
            var user = this.data
                .User
                .Where(x => x.Id == this.User.GetId())
                .Select(x => new ManageFormModel
                {
                    Address = x.Address,
                    City = x.City,
                    Email = x.Email,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    PhoneNumber = x.PhoneNumber
                })
                .FirstOrDefault();

            if (user == null)
            {
                return Redirect("/User/Profile");
            }

            return View(user);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Manage(ManageFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var oldUser = this.data.User.Where(x => x.Id == this.User.GetId()).FirstOrDefault();
            var userId = this.User.GetId();

            this.data.User.Remove(oldUser);

            var user = new User
            {
                Id = userId,
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                City = model.City,
                Address = model.Address,
                PhoneNumber = model.PhoneNumber,
                LastOnline = DateTime.Now,
                ProfileImage = this.fileService.UploadFile(model.ProfileImage)
            };

            this.data.User.Add(user);
            this.data.SaveChanges();

            return Redirect("/User/Profile");
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
                    ProfileImage = c.ProfileImage,
                    Listings = listings
                })
                .FirstOrDefault();

            ViewBag.Customer = customer;
            ViewBag.Listings = listings;

            return View();
        }
    }
}


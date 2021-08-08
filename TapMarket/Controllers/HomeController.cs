namespace TapMarket.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using TapMarket.Data;
    using TapMarket.Infrastructure;
    using TapMarket.Models;
    using TapMarket.Models.Home;
    using TapMarket.Models.Listing;

    public class HomeController : Controller
    {
        private readonly TapMarketDbContext data;

        public HomeController(TapMarketDbContext data)
        {
            this.data = data;
        }

        [HttpPost]
        public IActionResult Index(HomeFormModel homeInfo)
        {
            List<ListingViewModel> searchedListings = null;

            bool flag = true;

            if (!ModelState.IsValid)
            {
                homeInfo.Categories = this.GetCategories();
            }

            if (!string.IsNullOrEmpty(homeInfo.SearchInput))
            {
                flag = false;

                searchedListings = this.data
                    .Listings
                    .Where(l => l.Title.ToLower().Contains(homeInfo.SearchInput.ToLower())
                        && l.CategoryId == homeInfo.CategoryId)
                    .Select(l => new ListingViewModel
                    {
                        Id = l.Id,
                        Title = l.Title,
                        Price = l.Price,
                        Condition = l.Condition.Name,
                        ImageUrl = l.ImageUrl
                    })
                    .ToList();

            }

            if (homeInfo.CategoryId > 0)
            {
                if (searchedListings == null)
                {
                    searchedListings = this.data
                    .Listings
                    .Where(l => l.CategoryId == homeInfo.CategoryId)
                    .Select(l => new ListingViewModel
                    {
                        Id = l.Id,
                        Title = l.Title,
                        Price = l.Price,
                        Condition = l.Condition.Name,
                        ImageUrl = l.ImageUrl
                    })
                    .ToList();
                }
                else
                {
                    searchedListings = searchedListings.Where(l => l.CategoryId == homeInfo.CategoryId).ToList();
                }

                flag = false;
            }


            if (searchedListings == null || flag)
            {
                return Redirect("/Home/Index");
            }

            ViewBag.SearchedListings = searchedListings;
            ViewBag.Customer = this.data.Customers.Where(c => c.Id == this.User.GetId()).FirstOrDefault();

            return View(new HomeFormModel
            {
                Categories = this.GetCategories(),
            });
        }

        public IActionResult Index()
        {
            var listingsQuery = this.data.Listings.AsQueryable();

            var listings = listingsQuery
                .Select(l => new ListingViewModel
                {
                    Id = l.Id,
                    Title = l.Title,
                    Price = l.Price,
                    Condition = l.Condition.Name,
                    ImageUrl = l.ImageUrl
                }).ToList();

            var rnd = new Random();
            var shuffledListings = listings.OrderBy(c => rnd.Next()).ToList();

            ViewBag.ShuffledListings = shuffledListings;
            ViewBag.Customer = this.data.Customers.Where(c => c.Id == this.User.GetId()).FirstOrDefault();

            return View(new HomeFormModel
            {
                Categories = this.GetCategories(),
            });
        }

        public IActionResult Help() => View();

        [HttpPost]
        public IActionResult Help(HelpPageFormModel info)
        {
            if (!ModelState.IsValid)
            {
                return View(info);
            }


            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private IEnumerable<ListingCategoryViewModel> GetCategories()
         => this.data
            .Categories
            .Select(c => new ListingCategoryViewModel
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();
    }
}

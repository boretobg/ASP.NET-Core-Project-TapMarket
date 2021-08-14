namespace TapMarket.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using TapMarket.Data;
    using TapMarket.Infrastructure;
    using TapMarket.Models;
    using TapMarket.Models.Home;
    using TapMarket.Models.Listing;
    using TapMarket.Services;

    using static WebConstants;

    public class HomeController : Controller
    {
        private readonly TapMarketDbContext data;
        private readonly IEmailService emailService;

        public HomeController(TapMarketDbContext data, IEmailService emailService)
        {
            this.data = data;
            this.emailService = emailService;
        }

        public IActionResult Success()
            => View();

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
                            || l.Description.ToLower().Contains(homeInfo.SearchInput.ToLower()))
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
                    searchedListings.AddRange(searchedListings.Where(l => l.CategoryId == homeInfo.CategoryId).ToList());
                }

                flag = false;
            }


            if (searchedListings == null || flag)
            {
                return Redirect("/Home/Index");
            }

            ViewBag.SearchedListings = searchedListings;
            ViewBag.Customer = this.data.User.Where(c => c.Id == this.User.GetId()).FirstOrDefault();

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
                    ImageUrl = l.ImageUrl,
                    CreatedOn = l.CreatedOn
                })
                .OrderByDescending(c => c.CreatedOn)
                .ToList();

            var customer = this.data
                .User
                .Where(c => c.Id == this.User.GetId())
                .FirstOrDefault();

            ViewBag.Listings = listings;
            ViewBag.Customer = customer;

            return View(new HomeFormModel
            {
                Categories = this.GetCategories(),
            });
        }

        [Authorize]
        public IActionResult Help()
        {
            var senderName = this.data
                .User
                .Where(c => c.Id == this.User.GetId())
                .Select(x => $"{x.FirstName} {x.LastName}")
                .FirstOrDefault();

            if (senderName is null)
            {
                return Redirect("/Home/Index");
            }

            ViewBag.SenderName = senderName;

            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Help(HelpPageFormModel info)
        {
            if (!ModelState.IsValid)
            {
                return View(info);
            }

            string senderEmail = info.SenderEmail;
            string senderName = info.SenderName;

            string receiverEmail = info.ReceiverEmail;
            string receiverName = receiverEmail.Split('@')[0];

            string subject = info.Subject;
            string content = info.Content;
            
            emailService.SendEmail(senderName, senderEmail, receiverName, receiverEmail, subject, content).Wait();

            return Redirect("/Home/Success");
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

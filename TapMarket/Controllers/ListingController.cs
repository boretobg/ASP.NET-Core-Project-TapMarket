namespace TapMarket.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;
    using TapMarket.Data;
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
        public IActionResult Add(AddListingFormModel item)
        {
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

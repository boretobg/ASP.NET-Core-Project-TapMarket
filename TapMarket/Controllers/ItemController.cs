namespace TapMarket.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;
    using TapMarket.Data;
    using TapMarket.Models.Item;

    public class ItemController : Controller
    {
        private readonly TapMarketDbContext data;

        public ItemController(TapMarketDbContext data) 
            => this.data = data;



        public IActionResult Add()
            => View(new AddItemFormModel
            {
                Categories = this.GetCategories()
            });

        [HttpPost]
        public IActionResult Add(AddItemFormModel item)
        {
            return View();
        }

        private IEnumerable<ItemCategoryViewModel> GetCategories()
         => this.data
            .Categories
            .Select(c => new ItemCategoryViewModel
            {
                Id = c.Id,
                Name = c.Name
            })
            .ToList();
    }
}

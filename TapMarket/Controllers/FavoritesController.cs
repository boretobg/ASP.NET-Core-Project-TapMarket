namespace TapMarket.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using TapMarket.Data;
    using TapMarket.Infrastructure;
    using TapMarket.Models.Customer;
    using TapMarket.Services;

    public class FavoritesController : Controller
    {
        private readonly TapMarketDbContext data;
        private readonly IListingService listingService;

        public FavoritesController(TapMarketDbContext data, IListingService listingService)
        {
            this.data = data;
            this.listingService = listingService;
        }

        [Authorize]
        public IActionResult All(string customerId)
        {
            var listings = this.listingService.GetListings();

            return View();
        }
    }
}

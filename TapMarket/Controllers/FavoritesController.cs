namespace TapMarket.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using TapMarket.Data;
    using TapMarket.Infrastructure;
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
        public IActionResult All()
        {
            var listings = this.listingService.GetListings(this.User.GetId());

            return View(listings);
        }
    }
}

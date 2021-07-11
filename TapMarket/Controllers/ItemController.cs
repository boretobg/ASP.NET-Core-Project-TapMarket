namespace TapMarket.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class ItemController : Controller
    {
        public IActionResult Add()
            => View();
    }
}

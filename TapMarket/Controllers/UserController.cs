namespace TapMarket.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class UserController : Controller
    {
        public IActionResult Profile() => View();
    }
}

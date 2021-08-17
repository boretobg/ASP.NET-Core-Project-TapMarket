using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using TapMarket.Data;
using TapMarket.Data.Models;
using TapMarket.Infrastructure;

namespace TapMarket.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<LogoutModel> _logger;
        private readonly TapMarketDbContext data;

        public LogoutModel(SignInManager<User> signInManager, ILogger<LogoutModel> logger, TapMarketDbContext data)
        {
            _signInManager = signInManager;
            _logger = logger;
            this.data = data;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            this.data.User.Where(x => x.Id == this.User.GetId()).FirstOrDefault().LastOnline = DateTime.Now;

            this.data.SaveChanges();

            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return RedirectToPage();
            }
        }
    }
}

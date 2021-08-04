namespace TapMarket.Areas.Identity.Pages.Account
{
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using TapMarket.Data.Models;

    using static Data.DataConstants.Customer;

    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;

        public RegisterModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required]
            [StringLength(NameMaxLength, MinimumLength = NameMinLength,
                ErrorMessage = "{0} must be between {2} and {1} characters.")]
            public string FirstName { get; set; }

            [Required]
            [StringLength(NameMaxLength, MinimumLength = NameMinLength,
                ErrorMessage = "{0} must be between {2} and {1} characters.")]
            public string LastName { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, MinimumLength = 6,
                ErrorMessage = "{0} must be at least {2} and at max {1} characters long.")]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Required]
            [Phone]
            public string PhoneNumber { get; set; }

            [Required]
            [StringLength(CityMaxLength, MinimumLength = CityMinLength,
                ErrorMessage = "{0} must be between {2} and {1} characters.")]
            public string City { get; set; }

            [Required]
            [StringLength(AddressMaxLength, MinimumLength = AddressMinLength,
                ErrorMessage = "{0} must be between {2} and {1} characters.")]
            public string Address { get; set; }

            [Required]
            [Url]
            public string PictureUrl { get; set; }
        }

        public void OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            if (ModelState.IsValid)
            {
                var customer = new Customer
                {
                    UserName = Input.Email,
                    Email = Input.Email,
                    FirstName = Input.FirstName,
                    LastName = Input.LastName,
                    City = Input.City,
                    Address = Input.Address,
                    PhoneNumber = Input.PhoneNumber,
                    PictureUrl = Input.PictureUrl
                };

                var result = await userManager.CreateAsync(customer, Input.Password);

                if (result.Succeeded)
                {
                    await this.signInManager.SignInAsync(customer, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return Page();
        }
    }
}

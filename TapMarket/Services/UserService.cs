namespace TapMarket.Services
{
    using Microsoft.AspNetCore.Mvc;
    using System.Security.Claims;
    using TapMarket.Data;

    public class UserService : Controller, IUserService
    {
        private readonly TapMarketDbContext data;

        public UserService(TapMarketDbContext data) 
            => this.data = data;

        public string GetId()
            => this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
    }
}

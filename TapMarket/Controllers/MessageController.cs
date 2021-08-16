namespace TapMarket.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Linq;
    using TapMarket.Data;
    using TapMarket.Infrastructure;
    using TapMarket.Models.Message;

    using static TapMarket.Data.DataConstants;

    [Authorize]
    public class MessageController : Controller
    {
        private readonly TapMarketDbContext data;

        public MessageController(TapMarketDbContext data) 
            => this.data = data;


        [Authorize]
        [HttpPost]
        public IActionResult Details(MessageDetailsFormModel info)
        {
            if (info.Message != null)
            {
                var message = new TapMarket.Data.Models.Message
                {
                    Sender = this.data.User.Where(x => x.Id == info.UserId).FirstOrDefault(),
                    SenderId = info.UserId,
                    Receiver = this.data.User.Where(x => x.Id == info.ReceiverId).FirstOrDefault(),
                    ReceiverId = info.ReceiverId,
                    Text = info.Message,
                    SentOn = DateTime.UtcNow
                };

                this.data.Messages.Add(message);
                this.data.SaveChanges();
            }

            return View($"/Message/Details?receiverId={info.ReceiverId}");
        }

        [Authorize]
        public IActionResult Details(string receiverId)
        {
            var messages = this.data
                .Messages
                .Where(m => m.ReceiverId == receiverId && m.SenderId == this.User.GetId())
                .OrderBy(x => x.SentOn)
                .ToList();

            ViewBag.Messages = messages;
            ViewBag.SenderId = this.User.GetId();
            ViewBag.ReceiverId = receiverId;

           

            return View();
        }

        [Authorize]
        public IActionResult Index()
        {
            var messages = this.data
                .Messages
                .Where(m => m.Sender.Id == this.User.GetId() || m.Receiver.Id == this.User.GetId())
                .Select(m => new MessageIndexViewModel
                { 
                    Receiver = m.Receiver,
                    ReceiverPictureUrl = m.Receiver.PictureUrl,
                    //LastOnline = m.Receiver.LastOnline
                })
                .ToList();

            ViewBag.Messages = messages;

            return View();
        }
    }
}

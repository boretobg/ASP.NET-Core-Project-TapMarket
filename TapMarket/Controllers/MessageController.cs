namespace TapMarket.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
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
                  
                };

                this.data.SaveChanges();
            }

            return View($"/Message/Details?receiverId={info.ReceiverId}");
        }

        [Authorize]
        public IActionResult Details(string receiverId)
        {
            ViewBag.SenderId = this.User.GetId();
            ViewBag.ReceiverId = receiverId;

            return View();
        }

        [Authorize]
        public IActionResult Index()
        {
            var currentUser = this.data.User.Where(x => x.Id == this.User.GetId()).FirstOrDefault();    

            var messages = this.data
                .Messages
                .Where(x => x.ReceiverId == this.User.GetId() || x.SenderId == this.User.GetId())
                .ToList();

            var finalmessages = new List<MessageIndexViewModel>();

            foreach (var message in messages)
            {
                if (message.SenderId == currentUser.Id)
                {
                    var receiver = this.data.User.Where(x => x.Id == message.ReceiverId).FirstOrDefault();

                    finalmessages.Add(new MessageIndexViewModel
                    { 
                        Receiver = receiver,
                        ReceiverPictureUrl = message.Receiver.PictureUrl
                    });
                }
                else
                {
                    var sender = this.data.User.Where(x => x.Id == message.SenderId).FirstOrDefault();

                    finalmessages.Add(new MessageIndexViewModel
                    {
                        Receiver = sender,
                        ReceiverPictureUrl = message.Sender.PictureUrl
                    });
                }
            }

            ViewBag.Messages = finalmessages;

            return View();
        }
    }
}

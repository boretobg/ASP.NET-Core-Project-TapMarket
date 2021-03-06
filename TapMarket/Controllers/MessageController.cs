namespace TapMarket.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using TapMarket.Data;
    using TapMarket.Data.Models;
    using TapMarket.Infrastructure;
    using TapMarket.Models.Message;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

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
            var message = this.data
                .Messages
                .Where(x => x.Id == info.Id)
                .FirstOrDefault();

            if (info.ReceiverId == this.User.GetId())
            {
                var temp = info.ReceiverId;
                info.ReceiverId = info.UserId;
                info.UserId = temp;
            }

            if (info.Message != null)
            {
                var messageContent = new MessageContent
                {
                    Text = info.Message,
                    SentOn = DateTime.Now,
                    SenderId = info.UserId,
                    IsSeen = true
                };

                message.Content.Add(messageContent);

                this.data.MessageContents.Add(messageContent);
                this.data.Messages.Attach(message);
            }

            this.data.SaveChanges();

            return Redirect($"/Message/Details?messageId={info.Id}");
        }

        [Authorize]
        public IActionResult Details(int messageId)
        {
            var contents = this.data
                .MessageContents
                .Where(x => x.MessageId == messageId)
                .ToList();

            var receiverId = this.data
                .Messages
                .Where(x => x.Id == messageId)
                .Select(x => x.ReceiverId)
                .FirstOrDefault();

            var senderId = this.data
                .Messages
                .Where(x => x.Id == messageId)
                .Select(x => x.SenderId)
                .FirstOrDefault();

            if (receiverId == this.User.GetId())
            {
                var temp = receiverId;
                receiverId = senderId;
                senderId = temp;
            }

            ViewBag.Content = contents;
            ViewBag.SenderId = senderId;
            ViewBag.ReceiverId = receiverId;
            ViewBag.MessageId = messageId;
            ViewBag.SenderName = this.data.User.Where(x => x.Id == senderId).Select(x => $"{x.FirstName} {x.LastName}").FirstOrDefault();
            ViewBag.ReceiverName = this.data.User.Where(x => x.Id == receiverId).Select(x => $"{x.FirstName} {x.LastName}").FirstOrDefault();

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
                        Id = message.Id,
                        Receiver = receiver,
                        ReceiverProfileImage = message.Receiver.ProfileImage,
                        LastOnline = receiver.LastOnline.Date == DateTime.Today ? "Today" : receiver.LastOnline.ToString()
                    });
                }
                else
                {
                    var sender = this.data.User.Where(x => x.Id == message.SenderId).FirstOrDefault();


                    finalmessages.Add(new MessageIndexViewModel
                    {
                        Id = message.Id,
                        Receiver = sender,
                        ReceiverProfileImage = message.Sender.ProfileImage,
                        LastOnline = sender.LastOnline.Date == DateTime.Today ? "Today" : sender.LastOnline.ToString()
                    });
                }
            }

            ViewBag.Messages = finalmessages;

            return View();
        }
    }
}

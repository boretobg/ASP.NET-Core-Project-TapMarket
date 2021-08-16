namespace TapMarket.Models.Message
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using TapMarket.Data.Models;

    public class MessageIndexViewModel
    {
        public int Id { get; set; }

        [Required]
        [Url]
        public string ReceiverPictureUrl { get; set; }

        public User Receiver { get; set; }

        public DateTime LastOnline { get; set; }
    }
}

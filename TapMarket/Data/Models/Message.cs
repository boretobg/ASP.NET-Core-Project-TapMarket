﻿namespace TapMarket.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants.Message;

    public class Message
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(TextMaxLength)]
        public string Text { get; set; }

        public DateTime SentOn { get; set; }

        public string SenderId { get; set; }
        public User Sender { get; set; }

        public string ReceiverId { get; set; }
        public User Receiver { get; set; }
    }
}
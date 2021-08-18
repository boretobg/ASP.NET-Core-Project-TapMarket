namespace TapMarket.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants.Message;

    public class MessageContent
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(TextMaxLength)]
        public string Text { get; set; }

        public DateTime SentOn { get; set; }

        public string SenderId { get; set; }

        public int MessageId { get; set; }
        public Message Message { get; set; }

        public bool IsSeen { get; set; } = false;
    }
}

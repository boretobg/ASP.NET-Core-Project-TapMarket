namespace TapMarket.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Message
    {
        public int Id { get; init; }

        public string SenderId { get; set; }
        public User Sender { get; set; }

        public string ReceiverId { get; set; }
        public User Receiver { get; set; }

        [Required]
        public ICollection<MessageContent> Content { get; set; } = new List<MessageContent>();
    }
}

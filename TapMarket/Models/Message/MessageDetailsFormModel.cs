namespace TapMarket.Models.Message
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants.Message;

    public class MessageDetailsFormModel
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string ReceiverId { get; set; }

        [Required]
        [StringLength(TextMaxLength, MinimumLength = TextMinLength,
            ErrorMessage = "Message must be between {2} and {1} characters!")]
        public string Message { get; set; }
    }
}

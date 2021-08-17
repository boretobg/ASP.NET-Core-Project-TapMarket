namespace TapMarket.Models.Listing
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants.Message;

    public class ListingDetailFormModel
    {
        public int ListingId { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public string ReceiverId { get; set; }

        [Required]
        public string Command { get; set; }

        [Required]
        [StringLength(TextMaxLength, MinimumLength = TextMinLength, 
            ErrorMessage = "Message must be between {2} and {1} characters!")]
        public string Message { get; set; }

        public int MessageId { get; set; }

        public string Category { get; set; }
    }
}

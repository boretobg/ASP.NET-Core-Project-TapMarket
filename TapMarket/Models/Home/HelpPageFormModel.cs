namespace TapMarket.Models.Home
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants.HelpPage;

    public class HelpPageFormModel
    {
        [Required]
        [MaxLength(TitleMaxLength, ErrorMessage = "Subject can't be more than {0} characters.")]
        public string Subject { get; set; }

        [Required]
        public string SenderName { get; set; }

        [Required]
        [EmailAddress]
        public string SenderEmail { get; set; }

        [Required]
        [EmailAddress]
        public string ReceiverEmail { get; set; }

        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength,
            ErrorMessage = "Content must be between {2} and {1} characters.")]
        public string Content { get; set; }
    }
}

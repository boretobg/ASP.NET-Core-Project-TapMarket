namespace TapMarket.Models.Home
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants.HelpPage;

    public class HelpPageFormModel
    {
        [Required]
        [MaxLength(TitleMaxLength, ErrorMessage = "Title can't be more than {0} characters.")]
        public string Subject { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength,
            ErrorMessage = "Title must be between {2} and {1} characters.")]
        public string Content { get; set; }
    }
}

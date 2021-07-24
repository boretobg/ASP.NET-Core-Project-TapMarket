namespace TapMarket.Models.Listing
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants.Listing;

    public class AddListingFormModel
    {
        public int Id { get; init; }

        [Required]
        [StringLength(TitleMaxLength, MinimumLength = TitleMinLength, 
            ErrorMessage = "Title must be between {2} and {1} characters.")]
        public string Title { get; init; }

        [Required]
        [Url]
        public string ImageUrl { get; init; }

        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength,
            ErrorMessage = "Title must be between {2} and {1} characters.")]
        public string Description { get; init; }

        public decimal Price { get; init; }

        public int ConditionId { get; init; }

        public int CategoryId { get; init; }

        public IEnumerable<ListingCategoryViewModel> Categories { get; set; }

        public IEnumerable<ListingConditionViewModel> Conditions { get; set; }
    }
}

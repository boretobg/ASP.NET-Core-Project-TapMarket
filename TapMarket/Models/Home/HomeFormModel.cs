namespace TapMarket.Models.Home
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using TapMarket.Models.Listing;

    using static Data.DataConstants.HomePage;

    public class HomeFormModel
    {
        [Required]
        [MaxLength(SearchInputMaxLength)]
        public string SearchInput { get; set; }

        public int CategoryId { get; init; }

        public IEnumerable<ListingCategoryViewModel> Categories { get; set; }
    }
}

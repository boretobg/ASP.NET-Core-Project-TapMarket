namespace TapMarket.Models.Home
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants.HomePage;

    public class HomeFormModel
    {
        [Required]
        [MaxLength(SearchInputMaxLength)]
        public string SearchInput { get; set; }
    }
}

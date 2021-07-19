namespace TapMarket.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants.Category;

    public class Category
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(MaxLength)]
        public string Name { get; set; }

        public IEnumerable<Listing> Listings { get; set; } = new List<Listing>();
    }
}

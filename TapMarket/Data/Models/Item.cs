namespace TapMarket.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using TapMarket.Data.Enums;

    using static DataConstants;

    public class Item
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; }

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; }

        public ConditionType Condition { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}

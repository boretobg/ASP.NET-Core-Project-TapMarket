namespace TapMarket.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using static DataConstants.Listing;

    public class Listing
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; }

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; }

        public DateTime CreatedOn { get; set; }

        [Required]
        public string Condition { get; set; }

        [Column(TypeName = "decimal(18, 0)")]
        public decimal Price { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}

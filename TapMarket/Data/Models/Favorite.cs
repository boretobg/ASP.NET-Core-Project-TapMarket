namespace TapMarket.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Favorite
    {
        [Key]
        public int Id { get; init; }

        public int ListingId { get; set; }
        public Listing Listing { get; set; }

        public string CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}

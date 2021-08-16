namespace TapMarket.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Favorite
    {
        [Key]
        public int Id { get; init; }

        public int ListingId { get; set; }
        public Listing Listing { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
    }
}

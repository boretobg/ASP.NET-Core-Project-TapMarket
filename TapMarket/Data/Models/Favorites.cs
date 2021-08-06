namespace TapMarket.Data.Models
{
    public class Favorites
    {
        public int ListingId { get; set; }
        public Listing Listing { get; set; }

        public string CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}

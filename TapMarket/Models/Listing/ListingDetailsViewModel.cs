namespace TapMarket.Models.Listing
{
    using System;

    public class ListingDetailsViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public string Condition { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Category { get; set; }

        public bool IsFavorite { get; set; }
    }
}

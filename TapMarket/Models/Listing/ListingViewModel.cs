using System;

namespace TapMarket.Models.Listing
{
    public class ListingViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        public string Condition { get; set; }

        public int CategoryId { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}

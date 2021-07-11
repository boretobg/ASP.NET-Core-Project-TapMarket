using System.Collections.Generic;

namespace TapMarket.Models.Item
{
    public class AddItemFormModel
    {
        public int Id { get; init; }

        public string Title { get; init; }

        public string ImageUrl { get; init; }

        public string Description { get; init; }

        public decimal Price { get; init; }

        public string Condition { get; init; }

        public int CategoryId { get; init; }
        public IEnumerable<ItemCategoryViewModel> Categories { get; set; }
    }
}

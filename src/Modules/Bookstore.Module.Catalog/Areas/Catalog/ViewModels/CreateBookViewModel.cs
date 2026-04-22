using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bookstore.Module.Catalog.ViewModels
{
    public class CreateBookViewModel
    {
        public string Title { get; set; } = string.Empty;

        public string Author { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public int StockQuantity { get; set; }

        public int CategoryId { get; set; }

        public bool IsActive { get; set; } = true;

        public List<SelectListItem> Categories { get; set; } = new();
    }
}
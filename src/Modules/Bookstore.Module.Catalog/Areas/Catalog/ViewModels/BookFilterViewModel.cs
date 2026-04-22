using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bookstore.Module.Catalog.ViewModels
{
    public class BookFilterViewModel
    {
        public string? Keyword { get; set; }

        public int? CategoryId { get; set; }

        public bool? IsActive { get; set; }

        public List<SelectListItem> Categories { get; set; } = new();
    }
}
using Bookstore.Module.Catalog.Models;

namespace Bookstore.Module.Catalog.ViewModels
{
    public class BookListViewModel
    {
        public List<Book> Books { get; set; } = new();

        public BookFilterViewModel Filter { get; set; } = new();

        public PaginationViewModel Pagination { get; set; } = new();
    }
}
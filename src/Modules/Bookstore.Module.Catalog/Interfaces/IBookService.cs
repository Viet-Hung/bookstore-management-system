using Bookstore.Module.Catalog.Models;

namespace Bookstore.Module.Catalog.Interfaces
{
    public interface IBookService
    {
        List<Book> GetAllBooks();
    }
}
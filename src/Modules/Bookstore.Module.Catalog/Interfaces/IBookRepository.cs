using Bookstore.Module.Catalog.Models;

namespace Bookstore.Module.Catalog.Interfaces
{
    public interface IBookRepository
    {
        List<Book> GetAll();
    }
}
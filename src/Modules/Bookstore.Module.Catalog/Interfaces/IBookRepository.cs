using Bookstore.Module.Catalog.Models;

namespace Bookstore.Module.Catalog.Interfaces
{
    public interface IBookRepository
    {
        List<Book> GetAll();
        List<Category> GetCategories();
        void Add(Book book);
        Book? GetById(int id);
        void Update(Book book);
    }
}
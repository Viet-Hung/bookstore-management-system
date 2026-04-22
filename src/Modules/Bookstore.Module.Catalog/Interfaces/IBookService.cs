using Bookstore.Module.Catalog.Models;

namespace Bookstore.Module.Catalog.Interfaces
{
    public interface IBookService
    {
        List<Book> GetAllBooks();
        List<Category> GetCategories();
        void CreateBook(Book book);
        Book? GetById(int id);
        void UpdateBook(Book book);
        void DeactivateBook(int id);
    }
}
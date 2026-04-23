using Bookstore.Module.Catalog.Models;

namespace Bookstore.Module.Catalog.Interfaces
{
    public interface IBookService
    {
        List<Book> GetAllBooks();
        List<Book> GetFilteredBooks(string? keyword, int? categoryId, bool? isActive);
        List<Book> GetPagedFilteredBooks(string? keyword, int? categoryId, bool? isActive, int page, int pageSize);
        int CountFilteredBooks(string? keyword, int? categoryId, bool? isActive);
        List<Category> GetCategories();
        void CreateBook(Book book);
        Book? GetById(int id);
        void UpdateBook(Book book);
        void DeactivateBook(int id);
    }
}
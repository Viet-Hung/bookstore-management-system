using Bookstore.Module.Catalog.Interfaces;
using Bookstore.Module.Catalog.Models;

namespace Bookstore.Module.Catalog.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public List<Book> GetAllBooks()
        {
            return _bookRepository.GetAll();
        }

        public List<Category> GetCategories()
        {
            return _bookRepository.GetCategories();
        }

        public void CreateBook(Book book)
        {
            _bookRepository.Add(book);
        }

        public Book? GetById(int id)
        {
            return _bookRepository.GetById(id);
        }

        public void UpdateBook(Book book)
        {
            _bookRepository.Update(book);
        }

        public void DeactivateBook(int id)
        {
            _bookRepository.Deactivate(id);
        }

        public List<Book> GetFilteredBooks(string? keyword, int? categoryId, bool? isActive)
        {
            return _bookRepository.GetFiltered(keyword, categoryId, isActive);
        }

        public List<Book> GetPagedFilteredBooks(string? keyword, int? categoryId, bool? isActive, int page, int pageSize)
        {
            return _bookRepository.GetPagedFiltered(keyword, categoryId, isActive, page, pageSize);
        }

        public int CountFilteredBooks(string? keyword, int? categoryId, bool? isActive)
        {
            return _bookRepository.CountFiltered(keyword, categoryId, isActive);
        }
    }
}
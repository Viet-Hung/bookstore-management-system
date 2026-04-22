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
    }
}
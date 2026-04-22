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
    }
}
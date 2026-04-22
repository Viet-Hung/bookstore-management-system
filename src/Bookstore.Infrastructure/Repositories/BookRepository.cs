using Bookstore.Infrastructure.Data;
using Bookstore.Module.Catalog.Interfaces;
using Bookstore.Module.Catalog.Models;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly BookstoreDbContext _context;

        public BookRepository(BookstoreDbContext context)
        {
            _context = context;
        }

        public List<Book> GetAll()
        {
            return _context.Books
                .Include(x => x.Category)
                .OrderBy(x => x.Id)
                .ToList();
        }

        public List<Category> GetCategories()
        {
            return _context.Categories
                .OrderBy(x => x.Name)
                .ToList();
        }

        public void Add(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
        }

        public Book? GetById(int id)
        {
            return _context.Books.FirstOrDefault(x => x.Id == id);
        }

        public void Update(Book book)
        {
            _context.Books.Update(book);
            _context.SaveChanges();
        }
    }
}
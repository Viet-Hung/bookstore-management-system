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

        public void Deactivate(int id)
        {
            var book = _context.Books.FirstOrDefault(x => x.Id == id);

            if (book == null)
            {
                return;
            }

            book.IsActive = false;
            _context.SaveChanges();
        }

        public List<Book> GetFiltered(string? keyword, int? categoryId, bool? isActive)
        {
            var query = _context.Books
                .Include(x => x.Category)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                var normalizedKeyword = keyword.Trim().ToLower();

                query = query.Where(x =>
                    x.Title.ToLower().Contains(normalizedKeyword) ||
                    x.Author.ToLower().Contains(normalizedKeyword));
            }

            if (categoryId.HasValue && categoryId.Value > 0)
            {
                query = query.Where(x => x.CategoryId == categoryId.Value);
            }

            if (isActive.HasValue)
            {
                query = query.Where(x => x.IsActive == isActive.Value);
            }

            return query
                .OrderBy(x => x.Id)
                .ToList();
        }
    }
}
using Bookstore.Infrastructure.Data;
using Bookstore.Module.Catalog.Interfaces;
using Bookstore.Module.Catalog.Models;

namespace Bookstore.Infrastructure.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly BookstoreDbContext _context;

    public CategoryRepository(BookstoreDbContext context)
    {
        _context = context;
    }

    public List<Category> GetAll()
    {
        return _context.Categories
            .OrderBy(x => x.Name)
            .ToList();
    }

    public Category? GetById(int id)
    {
        return _context.Categories.FirstOrDefault(x => x.Id == id);
    }

    public bool ExistsByName(string name, int? excludeId = null)
    {
        return _context.Categories.Any(x =>
            x.Name == name &&
            (!excludeId.HasValue || x.Id != excludeId.Value));
    }

    public bool HasBooks(int categoryId)
    {
        return _context.Books.Any(x => x.CategoryId == categoryId);
    }

    public void Add(Category category)
    {
        _context.Categories.Add(category);
        _context.SaveChanges();
    }

    public void Update(Category category)
    {
        _context.Categories.Update(category);
        _context.SaveChanges();
    }
}
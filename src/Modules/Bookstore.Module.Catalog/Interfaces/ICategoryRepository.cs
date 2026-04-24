using Bookstore.Module.Catalog.Models;

namespace Bookstore.Module.Catalog.Interfaces;

public interface ICategoryRepository
{
    List<Category> GetAll();
    Category? GetById(int id);
    bool ExistsByName(string name, int? excludeId = null);
    bool HasBooks(int categoryId);
    void Add(Category category);
    void Update(Category category);
}
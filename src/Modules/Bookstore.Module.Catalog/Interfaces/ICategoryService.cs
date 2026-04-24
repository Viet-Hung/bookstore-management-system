using Bookstore.Module.Catalog.Models;

namespace Bookstore.Module.Catalog.Interfaces;

public interface ICategoryService
{
    List<Category> GetAll();
    Category? GetById(int id);
    bool Create(Category category);
    bool Update(Category category);
    bool Deactivate(int id);
}
using Bookstore.Module.Catalog.Interfaces;
using Bookstore.Module.Catalog.Models;

namespace Bookstore.Module.Catalog.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public List<Category> GetAll()
    {
        return _categoryRepository.GetAll();
    }

    public Category? GetById(int id)
    {
        return _categoryRepository.GetById(id);
    }

    public bool Create(Category category)
    {
        category.Name = category.Name.Trim();

        if (_categoryRepository.ExistsByName(category.Name))
        {
            return false;
        }

        category.IsActive = true;
        _categoryRepository.Add(category);

        return true;
    }

    public bool Update(Category category)
    {
        var existingCategory = _categoryRepository.GetById(category.Id);

        if (existingCategory == null)
        {
            return false;
        }

        var newName = category.Name.Trim();

        if (_categoryRepository.ExistsByName(newName, category.Id))
        {
            return false;
        }

        existingCategory.Name = newName;
        existingCategory.IsActive = category.IsActive;

        _categoryRepository.Update(existingCategory);

        return true;
    }

    public bool Deactivate(int id)
    {
        var category = _categoryRepository.GetById(id);

        if (category == null)
        {
            return false;
        }

        category.IsActive = false;
        _categoryRepository.Update(category);

        return true;
    }
}
using Bookstore.Module.Catalog.Interfaces;
using Bookstore.Module.Catalog.Models;
using Bookstore.Module.Catalog.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Module.Catalog.Areas.Catalog.Controllers;

[Area("Catalog")]
public class CategoriesController : Controller
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public IActionResult Index()
    {
        var categories = _categoryService.GetAll();
        return View(categories);
    }

    public IActionResult IndexNew()
    {
        var categories = _categoryService.GetAll();
        return View("Index.updated", categories);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View(new CreateCategoryViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(CreateCategoryViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var category = new Category
        {
            Name = model.Name
        };

        var created = _categoryService.Create(category);

        if (!created)
        {
            ModelState.AddModelError(nameof(model.Name), "Category name already exists.");
            return View(model);
        }

        TempData["SuccessMessage"] = "Category created successfully.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var category = _categoryService.GetById(id);

        if (category == null)
        {
            return NotFound();
        }

        var model = new EditCategoryViewModel
        {
            Id = category.Id,
            Name = category.Name,
            IsActive = category.IsActive
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(EditCategoryViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var category = new Category
        {
            Id = model.Id,
            Name = model.Name,
            IsActive = model.IsActive
        };

        var updated = _categoryService.Update(category);

        if (!updated)
        {
            ModelState.AddModelError(nameof(model.Name), "Category name already exists or category was not found.");
            return View(model);
        }

        TempData["SuccessMessage"] = "Category updated successfully.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Deactivate(int id)
    {
        var deactivated = _categoryService.Deactivate(id);

        if (!deactivated)
        {
            return NotFound();
        }

        TempData["SuccessMessage"] = "Category deactivated successfully.";
        return RedirectToAction(nameof(Index));
    }
}
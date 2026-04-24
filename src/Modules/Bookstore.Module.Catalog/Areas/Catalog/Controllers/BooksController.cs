// using Bookstore.Module.Catalog.Models;
using Bookstore.Module.Catalog.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Bookstore.Module.Catalog.Models;
using Bookstore.Module.Catalog.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bookstore.Module.Catalog.Areas.Catalog.Controllers
{
    [Area("Catalog")]
    public class BooksController : Controller
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }
        [HttpGet]
        // public IActionResult Index(string? keyword, int? categoryId, bool? isActive)
        public IActionResult Index(string? keyword, int? categoryId, bool? isActive, int page = 1)
        {
            // var books = new List<Book>
            // {
            //     new Book
            //     {
            //         Id = 1,
            //         Title = "Clean Code",
            //         Author = "Robert C. Martin",
            //         Price = 15.99m,
            //         StockQuantity = 10,
            //         CategoryId = 1,
            //         IsActive = true
            //     },
            //     new Book
            //     {
            //         Id = 2,
            //         Title = "The Pragmatic Programmer",
            //         Author = "Andrew Hunt, David Thomas",
            //         Price = 18.50m,
            //         StockQuantity = 7,
            //         CategoryId = 1,
            //         IsActive = true
            //     },
            //     new Book
            //     {
            //         Id = 3,
            //         Title = "Domain-Driven Design",
            //         Author = "Eric Evans",
            //         Price = 22.00m,
            //         StockQuantity = 4,
            //         CategoryId = 2,
            //         IsActive = true
            //     }
            // };
            // var books = _bookService.GetAllBooks();
            // var books = _bookService.GetFilteredBooks(keyword, categoryId, isActive);
            const int pageSize = 5;

            var totalItems = _bookService.CountFilteredBooks(keyword, categoryId, isActive);
            var books = _bookService.GetPagedFilteredBooks(keyword, categoryId, isActive, page, pageSize);
            var categories = _bookService.GetCategories();

            var model = new BookListViewModel
            {
                Books = books,
                Filter = new BookFilterViewModel
                {
                    Keyword = keyword,
                    CategoryId = categoryId,
                    IsActive = isActive,
                    Categories = categories.Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Name
                    }).ToList()
                },
                Pagination = new PaginationViewModel
                {
                    CurrentPage = page,
                    PageSize = pageSize,
                    TotalItems = totalItems
                }
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var categories = _bookService.GetCategories();

            var viewModel = new CreateBookViewModel
            {
                Categories = categories.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Create(CreateBookViewModel model)
        {
            if (model.Price <= 0)
            {
                ModelState.AddModelError(nameof(model.Price), "Price must be greater than 0.");
            }

            if (model.StockQuantity < 0)
            {
                ModelState.AddModelError(nameof(model.StockQuantity), "Stock quantity cannot be negative.");
            }

            if (!ModelState.IsValid)
            {
                model.Categories = _bookService.GetCategories()
                    .Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Name
                    }).ToList();

                TempData["ErrorMessage"] = "Book was not created.";

                return View(model);
            }

            var book = new Book
            {
                Title = model.Title,
                Author = model.Author,
                Price = model.Price,
                StockQuantity = model.StockQuantity,
                CategoryId = model.CategoryId,
                IsActive = model.IsActive
            };

            _bookService.CreateBook(book);

            TempData["SuccessMessage"] = "Book created successfully.";

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var book = _bookService.GetById(id);
            if (book == null)
                return NotFound();

            var categories = _bookService.GetCategories();

            var model = new EditBookViewModel
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                Price = book.Price,
                StockQuantity = book.StockQuantity,
                CategoryId = book.CategoryId,
                IsActive = book.IsActive,
                Categories = categories.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(EditBookViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = _bookService.GetCategories()
                    .Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Name
                    }).ToList();

                return View(model);
            }

            var book = _bookService.GetById(model.Id);
            if (book == null)
            {
                TempData["ErrorMessage"] = "Book was not found.";
                return NotFound();
            }

            book.Title = model.Title;
            book.Author = model.Author;
            book.Price = model.Price;
            book.StockQuantity = model.StockQuantity;
            book.CategoryId = model.CategoryId;
            book.IsActive = model.IsActive;

            _bookService.UpdateBook(book);

            TempData["SuccessMessage"] = "Book updated successfully.";

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Deactivate(int id)
        {
            var book = _bookService.GetById(id);
            if (book == null)
            {
                TempData["ErrorMessage"] = "Book was not found.";
                return NotFound();
            }

            _bookService.DeactivateBook(id);

            TempData["SuccessMessage"] = "Book deactivated successfully.";

            return RedirectToAction("Index");
        }
    }
}
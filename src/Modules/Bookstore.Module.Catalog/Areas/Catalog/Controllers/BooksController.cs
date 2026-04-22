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
        public IActionResult Index()
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
            var books = _bookService.GetAllBooks();

            return View(books);
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

            return RedirectToAction("Index");
        }
    }
}
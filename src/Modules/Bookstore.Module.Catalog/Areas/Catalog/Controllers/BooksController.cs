using Bookstore.Module.Catalog.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Module.Catalog.Areas.Catalog.Controllers
{
    [Area("Catalog")]
    public class BooksController : Controller
    {
        public IActionResult Index()
        {
            var books = new List<Book>
            {
                new Book
                {
                    Id = 1,
                    Title = "Clean Code",
                    Author = "Robert C. Martin",
                    Price = 15.99m,
                    StockQuantity = 10,
                    CategoryId = 1,
                    IsActive = true
                },
                new Book
                {
                    Id = 2,
                    Title = "The Pragmatic Programmer",
                    Author = "Andrew Hunt, David Thomas",
                    Price = 18.50m,
                    StockQuantity = 7,
                    CategoryId = 1,
                    IsActive = true
                },
                new Book
                {
                    Id = 3,
                    Title = "Domain-Driven Design",
                    Author = "Eric Evans",
                    Price = 22.00m,
                    StockQuantity = 4,
                    CategoryId = 2,
                    IsActive = true
                }
            };

            return View(books);
        }
    }
}
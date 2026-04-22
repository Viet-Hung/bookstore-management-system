using Bookstore.Module.Catalog.Models;

namespace Bookstore.Infrastructure.Data
{
    public static class DbInitializer
    {
        public static void Seed(BookstoreDbContext context)
        {
            if (context.Categories.Any() || context.Books.Any())
            {
                return;
            }

            var softwareCategory = new Category { Name = "Software Engineering" };
            var architectureCategory = new Category { Name = "Architecture" };

            context.Categories.AddRange(softwareCategory, architectureCategory);
            context.SaveChanges();

            var books = new List<Book>
            {
                new Book
                {
                    Title = "Clean Code",
                    Author = "Robert C. Martin",
                    Price = 15.99m,
                    StockQuantity = 10,
                    CategoryId = softwareCategory.Id,
                    IsActive = true
                },
                new Book
                {
                    Title = "The Pragmatic Programmer",
                    Author = "Andrew Hunt, David Thomas",
                    Price = 18.50m,
                    StockQuantity = 7,
                    CategoryId = softwareCategory.Id,
                    IsActive = true
                },
                new Book
                {
                    Title = "Domain-Driven Design",
                    Author = "Eric Evans",
                    Price = 22.00m,
                    StockQuantity = 4,
                    CategoryId = architectureCategory.Id,
                    IsActive = true
                }
            };

            context.Books.AddRange(books);
            context.SaveChanges();
        }
    }
}
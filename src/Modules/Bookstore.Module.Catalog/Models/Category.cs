namespace Bookstore.Module.Catalog.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;

        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
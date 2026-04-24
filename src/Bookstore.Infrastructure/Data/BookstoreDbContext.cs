using Microsoft.EntityFrameworkCore;
using Bookstore.Module.Catalog.Models;
using Bookstore.Module.Orders.Models;

namespace Bookstore.Infrastructure.Data
{
    public class BookstoreDbContext : DbContext
    {
        public BookstoreDbContext(DbContextOptions<BookstoreDbContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books => Set<Book>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Categories");
                entity.HasKey(x => x.Id);

                entity.Property(x => x.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.ToTable("Books");
                entity.HasKey(x => x.Id);

                entity.Property(x => x.Title)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(x => x.Author)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(x => x.Price)
                    .HasColumnType("decimal(18,2)");

                entity.Property(x => x.StockQuantity)
                    .IsRequired();

                entity.Property(x => x.IsActive)
                    .IsRequired();

                // entity.HasOne<Category>()
                //     .WithMany()
                //     .HasForeignKey(x => x.CategoryId)
                //     .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(x => x.Category)
                    .WithMany(x => x.Books)
                    .HasForeignKey(x => x.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Order>(entity =>
{
    entity.HasKey(x => x.Id);

    entity.Property(x => x.TotalAmount)
        .HasColumnType("decimal(18,2)");

    entity.Property(x => x.Status)
        .HasConversion<int>();

    entity.HasMany(x => x.Items)
        .WithOne(x => x.Order)
        .HasForeignKey(x => x.OrderId)
        .OnDelete(DeleteBehavior.Cascade);
});

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.Property(x => x.UnitPrice)
                    .HasColumnType("decimal(18,2)");

                entity.HasOne(x => x.Book)
                    .WithMany()
                    .HasForeignKey(x => x.BookId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
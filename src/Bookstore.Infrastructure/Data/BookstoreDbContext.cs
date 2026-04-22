using Bookstore.Module.Catalog.Models;
using Microsoft.EntityFrameworkCore;

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
        }
    }
}
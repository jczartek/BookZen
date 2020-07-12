using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public class DbCoreContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<BookRental> BookRentals { get; set; }

        public DbCoreContext(DbContextOptions<DbCoreContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .Property(p => p.Title)
                .IsRequired();

            modelBuilder.Entity<Book>()
                .Property(p => p.YearOfPublication)
                .IsRequired(false);

            modelBuilder.Entity<Book>()
                .Property(p => p.ReadDate)
                .IsRequired(false);

            modelBuilder.Entity<BookAuthor>()
                .HasKey(obj => new { obj.BookId, obj.AuthorId });

            modelBuilder.Entity<Author>()
                .Property(p => p.Name)
                .IsRequired();
        }
    }
}

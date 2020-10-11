using DataLayer.Configurations;
using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public class DbCoreContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Borrower> Borrowers { get; set; }

        public DbCoreContext(DbContextOptions<DbCoreContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(ConfigFactory.ConfigFor<Book>());
            modelBuilder.ApplyConfiguration(ConfigFactory.ConfigFor<BookAuthor>());
        }
    }
}

using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Text;

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
                .Property(p => p.YearOfPublication)
                .IsRequired(false);

            modelBuilder.Entity<Book>()
                .Property(p => p.ReadDate)
                .IsRequired(false);

            modelBuilder.Entity<BookAuthor>()
                .HasKey(obj => new { obj.BookId, obj.AuthorId });

            modelBuilder.Entity<BookAuthor>()
                .HasOne(pt => pt.Book)
                .WithMany(p => p.AuthorsLink)
                .HasForeignKey(pt => pt.BookId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BookAuthor>()
                .HasOne(pt => pt.Author)
                .WithMany(t => t.BooksLink)
                .HasForeignKey(pt => pt.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

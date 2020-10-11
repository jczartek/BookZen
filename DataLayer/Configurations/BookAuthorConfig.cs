using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataLayer.Configurations
{
    public class BookAuthorConfig : IEntityTypeConfiguration<BookAuthor>
    {
        public void Configure(EntityTypeBuilder<BookAuthor> entity)
        {
            entity.HasKey(p => new { p.BookId, p.AuthorId });

            entity.HasOne(p => p.Book)
                .WithMany(p => p.AuthorsLink)
                .HasForeignKey(p => p.BookId);

            entity.HasOne(p => p.Author)
                .WithMany(p => p.BooksLink)
                .HasForeignKey(p => p.AuthorId);
        }
    }
}

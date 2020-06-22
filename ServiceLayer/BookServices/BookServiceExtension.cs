using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ServiceLayer.BookServices
{
    static class BookServiceExtension
    {
        public static BookDto MapBookToBookDto(this Book book)
        {
            var bookDto = new BookDto()
            {
                BookId = book.BookId,
                Title = book.Title,
                Description = book.Description,
                YearOfPublication = book.DateOfPublication,
                Publisher = book.Publisher,
                Isbn = book.Isbn,
                IsRead = book.IsRead,
                ReadDate = book.ReadDate,
                Authors = string.Join(", ",
                book.AuthorsLink
                  .Select(q => q.Author.Name)),
                IsOnLoan = book.BookRental != null
            };

            return bookDto;
        }

        private static List<BookAuthor> CreateBookAuthors(Book book, string authors)
        {
            var bookAuthors = new List<BookAuthor>();

            foreach (var author in authors.Split(','))
            {
                var bookAuthor = new BookAuthor() { Book = book};
                var newAuthor = AuthorService.FindAuthorByName(author.Trim());

                if (newAuthor != null)
                    bookAuthor.AuthorId = newAuthor.AuthorId;
                else
                    bookAuthor.Author = new Author { Name = author.Trim() };
                bookAuthors.Add(bookAuthor);
            }

            return bookAuthors;
        }
        public static Book MapBookDtoToBook(this BookDto bookDto)
        {
            var book = new Book()
            {
                BookId = bookDto.BookId,
                Title = bookDto.Title,
                Description = bookDto.Description,
                DateOfPublication = bookDto.YearOfPublication,
                Publisher = bookDto.Publisher,
                Isbn = bookDto.Isbn,
                IsRead = bookDto.IsRead,
                ReadDate = bookDto.ReadDate
            };

            if (!String.IsNullOrWhiteSpace(bookDto.Authors))
                book.AuthorsLink = CreateBookAuthors(book, bookDto.Authors);

            return book;
        }
    }
}

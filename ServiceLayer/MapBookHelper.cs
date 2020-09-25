using DataLayer;
using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.BookServices;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ServiceLayer
{
    public static class MapBookHelper
    {

        private static BookAuthor GetBookAuthorById(int bookId, int authorId)
        {
            using (var ctx = DbCoreContextFactory.Create())
            {
                return ctx.Books.Include(i => i.AuthorsLink)
                                .ThenInclude(i => i.Author)
                                .Single(x => x.BookId == bookId)
                                .AuthorsLink
                                .SingleOrDefault(x => x.BookId == bookId && x.AuthorId == authorId);
            }
        }

        private static void DeleteAllBookAuthorsByBookId(int bookId)
        {
            using (var ctx = DbCoreContextFactory.Create())
            {
                var bookAuthors = ctx.Books.Include(i => i.AuthorsLink)
                                .Single(x => x.BookId == bookId)
                                .AuthorsLink;

                if (bookAuthors != null)
                {
                    ctx.RemoveRange(bookAuthors);
                    ctx.SaveChanges();
                }
            }
        }

        private static List<BookAuthor> CreateBookAuthors(Book book, string authors)
        {
            var bookAuthors = new List<BookAuthor>();

            if (book.BookId > 0)
                DeleteAllBookAuthorsByBookId(book.BookId);

            foreach (var athr in authors.Split(','))
            {
                var author = AuthorService.FindAuthorByName(athr.Trim());
                BookAuthor bookAuthor = null;

                if (author != null && book.BookId != 0)
                {
                    bookAuthor = GetBookAuthorById(book.BookId, author.AuthorId);
                }

                if (bookAuthor == null)
                {
                    bookAuthor = new BookAuthor() { Book = book };

                    if (author != null)
                        bookAuthor.AuthorId = author.AuthorId;
                    else
                        bookAuthor.Author = new Author { Name = athr.Trim() };
                }

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
                YearOfPublication = bookDto.YearOfPublication,
                Publisher = bookDto.Publisher,
                Isbn = bookDto.Isbn,
                IsRead = bookDto.IsRead,
                ReadDate = bookDto.ReadDate
            };

            if (!String.IsNullOrWhiteSpace(bookDto.Authors))
                book.AuthorsLink = CreateBookAuthors(book, bookDto.Authors);

            var bookRental = ServiceFactory.CreateBookRentalService((service, bookId) =>
            {
                return service.GetBookRentalByBookId(bookId);
            }, book.BookId);

            if (bookDto.IsOnLoan || bookRental != null)
            {

                if (bookRental != null && bookDto.IsOnLoan)
                {
                    if (bookRental.Name != bookDto.NameOfBorrower || bookRental.DateBorrowing != bookDto.DateBorrowing)
                    {
                        bookRental.Name = bookDto.NameOfBorrower;
                        bookRental.DateBorrowing = bookDto.DateBorrowing;
                        ServiceFactory.CreateBookRentalService(service => service.UpdateBookRental(bookRental));
                    }
                }
                else if (bookRental != null && !bookDto.IsOnLoan)
                {
                    ServiceFactory.CreateBookRentalService(service => service.DeleteBookRental(bookRental));
                    bookRental = null;
                }
                else
                {
                    bookRental = new BookRental
                    {
                        BookId = book.BookId,
                        Name = bookDto.NameOfBorrower,
                        DateBorrowing = bookDto.DateBorrowing
                    };
                }
                book.BookRental = bookRental;
            }

            return book;
        }
    }
}

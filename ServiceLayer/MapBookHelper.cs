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
        public static BookDto MapBookToBookDto(this Book book)
        {
            var bookDto = new BookDto()
            {
                BookId = book.BookId,
                Title = book.Title,
                Description = book.Description,
                YearOfPublication = book.YearOfPublication,
                Publisher = book.Publisher,
                Isbn = book.Isbn,
                IsRead = book.IsRead,
                ReadDate = book.ReadDate,
                Authors = string.Join(", ",
                book.AuthorsLink
                  .Select(q => q.Author.Name)),
                IsOnLoan = book.BookRental != null
            };

            if (bookDto.IsOnLoan)
            {
                bookDto.NameOfBorrower = book.BookRental.Name;
                bookDto.DateBorrowing = book.BookRental.DateBorrowing;
            }

            return bookDto;
        }

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

        private static BookRental GetBookRentalByBookId(int bookId)
        {
            if (bookId == 0)
                return null;

            using (var ctx = DbCoreContextFactory.Create())
            {
                return ctx.BookRentals.SingleOrDefault(x => x.BookId == bookId);
            }
        }

        private static bool HasBookRental(int bookId)
        {
            return GetBookRentalByBookId(bookId) != null;
        }

        private static void UpdateBookRental(BookRental bookRental)
        {
            using (var ctx = DbCoreContextFactory.Create())
            {
                ctx.BookRentals.Update(bookRental);
                ctx.SaveChanges();
            }
        }

        private static void DeleteBookRental(BookRental bookRental)
        {
            using (var ctx = DbCoreContextFactory.Create())
            {
                ctx.BookRentals.Remove(bookRental);
                ctx.SaveChanges();
            }
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


            var bookRental = GetBookRentalByBookId(book.BookId);
            if (bookDto.IsOnLoan || bookRental != null)
            {

                if (bookRental != null && bookDto.IsOnLoan)
                {
                    if (bookRental.Name != bookDto.NameOfBorrower || bookRental.DateBorrowing != bookDto.DateBorrowing)
                    {
                        bookRental.Name = bookDto.NameOfBorrower;
                        bookRental.DateBorrowing = bookDto.DateBorrowing;
                        UpdateBookRental(bookRental);
                    }
                }
                else if (bookRental != null && !bookDto.IsOnLoan)
                {
                    DeleteBookRental(bookRental);
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

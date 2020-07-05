using DataLayer;
using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceLayer.BookServices
{
    public class BookService
    {
        private BookDto BookDto;
        
        public static BookService Init()
        {
            return new BookService();
        }

        private BookService()
        {
            BookDto = new BookDto();
        }

        public BookService Id(int id)
        {
            BookDto.BookId = id;
            return this;
        }

        public BookService Title(string title)
        {
            if (String.IsNullOrEmpty(title))
                throw new ArgumentException("Argument title can't be empty or null");

            BookDto.Title = title;
            return this;
        }

        public BookService Authors(string authors)
        {
            BookDto.Authors = authors;
            return this;
        }

        public BookService Publisher(string publisher)
        {
            BookDto.Publisher = publisher;
            return this;
        }

        public BookService Isbn(string isbn)
        {
            BookDto.Isbn = isbn;
            return this;
        }

        public BookService Description(string description)
        {
            BookDto.Description = description;
            return this;
        }

        public BookService YearOfPublication(int? yearOfPublication)
        {
            BookDto.YearOfPublication = yearOfPublication;
            return this;
        }

        public BookService IsBookRead(bool isRead, Action<BookDto> action = null)
        {
            if (isRead) action(BookDto);
            return this;
        }

        public BookService IsBookOnLoan(bool isOnLoan, Action<BookDto> action = null)
        {
            if (isOnLoan) action(BookDto);
            return this;
        }

        public BookDto SaveToDatabase()
        {
            using (var dbContext = DbCoreContextFactory.Create())
            {
                
                var book = BookDto.MapBookDtoToBook();
                if (book.BookId == default) dbContext.Add(book);
                else dbContext.Update(book);
                dbContext.SaveChanges();

                BookDto.BookId = book.BookId;
                return BookDto;
            }
        }

        public static BookDto FindBookByIsbn(string isbn)
        {
            using (var dbContext = DbCoreContextFactory.Create())
            {
                try
                {
                    var dto =  dbContext.Books
                        .Include(b => b.AuthorsLink)
                            .ThenInclude(aL => aL.Author)
                        .Include(b => b.BookRental)
                        .Single(b => b.Isbn == isbn)
                        .MapBookToBookDto();

                    return dto;
                }
                catch
                {
                    return null;
                }
            }
        }

        public static List<BookDto> GetAllBooks()
        {
            using (var dbContext = DbCoreContextFactory.Create())
            {
                return dbContext.Books
                    .AsNoTracking()
                    .Include(i => i.AuthorsLink)
                    .ThenInclude(i => i.Author)
                    .Include(i => i.BookRental)
                    .Select(x => x.MapBookToBookDto())
                    .ToList();
            }
        }

        public static void DeleteBook(int bookId)
        {
            using (var dbContext = DbCoreContextFactory.Create())
            {
                Book book = dbContext.Books.Find(bookId);

                if (book != null)
                {
                    dbContext.Books.Remove(book);
                    dbContext.SaveChanges();
                }
            }
        }

        public static BookDto FindBookById(int bookId)
        {
            using (var dbContext = DbCoreContextFactory.Create())
            {
                return dbContext.Books
                    .Include(i => i.AuthorsLink)
                    .ThenInclude(i => i.Author)
                    .Include(i => i.BookRental)
                    .Single(b => b.BookId == bookId)
                    .MapBookToBookDto();
            }
        }

        public static void UpdateBook(BookDto dto)
        {
            if (dto != null)
            {
                using (var dbContext = DbCoreContextFactory.Create())
                {
                    var book = dto.MapBookDtoToBook();
                    dbContext.Update(book);
                    dbContext.SaveChanges();
                }
            }
        }

    }
}

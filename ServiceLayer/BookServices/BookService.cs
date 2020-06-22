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

        public BookService YearOfPublication(int yearOfPublication)
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
                dbContext.Add(book);
                dbContext.SaveChanges();
            }
            return BookDto;
        }

        public static BookDto FindBookByIsbn(string isbn)
        {
            using (var dbContext = DbCoreContextFactory.Create())
            {
                try
                {
                    var dto =  dbContext.Books
                        .AsNoTracking()
                        .Include(b => b.AuthorsLink)
                            .ThenInclude(aL => aL.Author)
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
                    .Select(x => x.MapBookToBookDto())
                    .ToList();
            }
        }
    }
}

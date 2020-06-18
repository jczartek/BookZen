using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceLayer.BookServices
{
    public class BookDto
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int YearOfPublication { get; set; }
        public string Publisher { get; set; }
        public string Isbn { get; set; }
        public bool IsRead { get; set; }
        public DateTime ReadDate { get; set; }

        public string Authors { get; set; }
        
        public bool IsOnLoan { get; set; }
        public string NameOfBorrower { get; set; }
        public DateTime DateBorrowing { get; set; }

        public static BookDto MapBookToBookDto(Book book)
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
                  .OrderBy(q => q.Author.Name)
                  .Select(q => q.Author.Name)),
                IsOnLoan = book.BookRental != null
            };

            return bookDto;
        }

        public static Book MapBookDtoToBook(BookDto bookDto)
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
                ReadDate = bookDto.ReadDate,
            };

            return book;
        }

    }
}

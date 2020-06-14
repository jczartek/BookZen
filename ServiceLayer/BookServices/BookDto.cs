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
        public int DateOfPublication { get; set; }
        public string Publisher { get; set; }
        public string Isbn { get; set; }
        public bool IsRead { get; set; }
        public DateTime ReadDate { get; set; }

        public string Authors { get; set; }
        
        public bool IsOnLoan { get; set; }
        public string NameOfBorrower { get; set; }
        public DateTime DateBorrowing { get; set; }

        public static BookDto MapBookDtoToBook(Book book)
        {
            var bookDto = new BookDto()
            {
                BookId = book.BookId,
                Title = book.Title,
                Description = book.Description,
                DateOfPublication = book.DateOfPublication,
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

    }
}

using ServiceLayer.BookServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer
{
    public interface IBookService : IDisposable
    {
        void AddBook(BookDto dto);
        void UpdateBook(BookDto dto);
        BookDto GetBookById(int id);
        void DeleteBookById(int id);
        List<BookDto> GetAllBooks();
        BookDto GetBookByIsbn(string isbn);
    }
}

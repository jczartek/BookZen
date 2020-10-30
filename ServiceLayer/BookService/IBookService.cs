using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public interface IBookService : IDisposable
    {
        void AddBook(BookDto dto);
        void UpdateBook(BookDto dto);
        Task<BookDto> GetBookById(int id);
        void DeleteBookById(int id);
        List<BookDto> GetAllBooks();
        Task<List<BookDto>> GetAllBooksAsync();
        BookDto GetBookByIsbn(string isbn);
    }
}

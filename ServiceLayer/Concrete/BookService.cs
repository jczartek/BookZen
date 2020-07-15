using DataLayer.Entities;
using RepositoryLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ServiceLayer.Concrete
{
    public class BookService : BaseService<Book>, IBookService
    {
        public BookService(IRepository<Book> repository) : base(repository) { }
        
        public void AddBook(BookDto dto)
        {
            if (dto != null)
            {
                var book = dto.MapBookDtoToBook();
                Repository.Add(book);
                dto.BookId = book.BookId;
            }
        }

        public void DeleteBookById(int id)
        {
            var book = Repository.GetById(id);

            if (book != null) Repository.Delete(book);
        }

        public List<BookDto> GetAllBooks()
        {
            return Repository
                .GetAll()
                .Select(x => x.MapBookToBookDto())
                .ToList();
        }

        public BookDto GetBookById(int id)
        {
            var book = Repository.GetById(id);

            if (book != null)
                return book.MapBookToBookDto();

            return null;
        }

        public BookDto GetBookByIsbn(string isbn)
        {
            var book = (Repository as IBookRepository).GetBookByIsbn(isbn);

            if (book != null)
                return book.MapBookToBookDto();

            return null;
        }

        public void UpdateBook(BookDto dto)
        {
            if (dto != null)
            {
                var book = dto.MapBookDtoToBook();
                Repository.Update(book);
            }
        }
    }
}

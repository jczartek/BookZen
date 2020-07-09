using RepositoryLayer;
using System;
using System.Collections.Generic;
using System.Linq;

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
            var book = BookDto.MapBookDtoToBook();
            if (book.BookId == default) AddBook(BookDto);
            else UpdateBook(BookDto);

            return BookDto;
        }

        public static BookDto FindBookByIsbn(string isbn)
        {
            var book = RepositoryFactory
                .CreateBookRepository()
                .GetBookByIsbn(isbn);

            if (book != null)
                return book.MapBookToBookDto();

            return null;
        }

        public static List<BookDto> GetAllBooks()
        {
            return RepositoryFactory
                .CreateBookRepository()
                .GetAll()
                .Select(x => x.MapBookToBookDto())
                .ToList();
        }

        public static void DeleteBook(int bookId)
        {
            var repository = RepositoryFactory.CreateBookRepository();
            var book = repository.GetById(bookId);

            if (book != null) repository.Delete(book);
        }

        public static BookDto FindBookById(int bookId)
        {
            var book = RepositoryFactory
                .CreateBookRepository()
                .GetById(bookId);

            if (book != null)
                return book.MapBookToBookDto();

            return null;
        }

        public static void UpdateBook(BookDto dto)
        {
            if (dto != null)
            {
                var book = dto.MapBookDtoToBook();
                RepositoryFactory
                    .CreateBookRepository()
                    .Update(book);
            }
        }

        public static void AddBook(BookDto dto)
        {
            if (dto != null)
            {
                var book = dto.MapBookDtoToBook();
                RepositoryFactory
                    .CreateBookRepository()
                    .Add(book);
                dto.BookId = book.BookId;
            }
        }

    }
}

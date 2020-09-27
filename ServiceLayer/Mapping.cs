using AutoMapper;
using DataLayer.Entities;
using RepositoryLayer.Abstract;
using RepositoryLayer.Concrete;
using ServiceLayer.BookServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceLayer
{
    public static class Mapping
    {
        private static readonly IBookRepository bookRepository = new BookRepository();
        private static MapperConfiguration mapperConfiguration = null;
        private static Mapper mapper = null;
        public static void Init()
        {
            Configure();
        }

        private static void Configure()
        {
            if (mapperConfiguration != null) return;

            mapperConfiguration = new MapperConfiguration(cfg =>
            {
                _ = cfg.CreateMap<Book, BookDto>()
                .ForMember(d => d.Authors, ops => ops.MapFrom(s => string.Join(",", s.AuthorsLink.Select(a => a.Author.Name))))
                .ForMember(d => d.IsOnLoan, ops => ops.MapFrom(s => s.BookRental != null))
                .ForMember(d => d.NameOfBorrower, ops => ops.MapFrom(s => s.BookRental != null ? s.BookRental.Name : default))
                .ForMember(d => d.DateBorrowing, ops => ops.MapFrom(s => s.BookRental != null ? s.BookRental.DateBorrowing : default))
                .ReverseMap()
                .ForPath(d => d.AuthorsLink, ops => ops.MapFrom(src => MapBookAutor(src)))
                .ForPath(d => d.BookRental, ops => ops.MapFrom(src => MapBookRental(src)));
            });
        }

        public static Mapper Mapper()
        {
            if (mapperConfiguration == null) throw new NullReferenceException("the config of the mapper can't be null. Return Mapping.Init() to initialize mapper.");

            return mapper ??= new Mapper(mapperConfiguration);

        }

        public static ICollection<BookAuthor> MapBookAutor(BookDto source)
        {
            var bookAuthors = new List<BookAuthor>();

            if (source.BookId > 0) bookRepository.DeleteAllBookAuthorsByBookId(source.BookId);

            foreach (var athr in source.Authors.Split(','))
            {
                var author = AuthorService.FindAuthorByName(athr.Trim());

                var bookAuthor = new BookAuthor() { BookId = source.BookId };

                if (author != null) bookAuthor.AuthorId = author.AuthorId;
                else bookAuthor.Author = new Author { Name = athr.Trim() };

                bookAuthors.Add(bookAuthor);
            }
            return bookAuthors;
        }

        private static BookRental MapBookRental(BookDto source)
        {
            if (source.BookId > 0)
                bookRepository.DeleteBookRentalByBookId(source.BookId);

            if (!source.IsOnLoan) return null;
           
            return new BookRental()
            {
               BookId = source.BookId,
               DateBorrowing = source.DateBorrowing,
               Name = source.NameOfBorrower
            };
        }
    }
}

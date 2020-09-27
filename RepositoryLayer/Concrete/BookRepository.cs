using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace RepositoryLayer.Concrete
{
    public class BookRepository : Repository, IBookRepository
    {
        public void Add(Book entity)
        {
            ctx.Add(entity);
            ctx.SaveChanges();
        }

        public void Delete(Book entity)
        {
            ctx.Remove(entity);
            ctx.SaveChanges();
        }

        public void DeleteAllBookAuthorsByBookId(int bookId)
        {
            var bookAuthors = ctx.Books.Include(i => i.AuthorsLink)
                .Single(x => x.BookId == bookId)
                .AuthorsLink;

            if (bookAuthors == null) return;

            ctx.RemoveRange(bookAuthors);
            ctx.SaveChanges();
        }

        public void DeleteBookRentalByBookId(int bookId)
        {
            var bookRental = ctx.Books.Include(i => i.BookRental)
                .Single(x => x.BookId == bookId)
                .BookRental;

            if (bookRental == null) return;

            ctx.Remove(bookRental);
            ctx.SaveChanges();
        }

        public List<Book> GetAll()
        {
            return ctx.Books
                      .AsNoTracking()
                      .Include(x => x.AuthorsLink)
                          .ThenInclude(x => x.Author)
                      .Include(x => x.BookRental)
                      .ToList();
        }

        public Book GetBookByIsbn(string isbn)
        {
            return ctx.Books
                .AsNoTracking()
                .Include(x => x.AuthorsLink)
                .ThenInclude(x => x.Author)
                .Include(x => x.BookRental)
                .SingleOrDefault(x => x.Isbn == isbn);
        }

        public Book GetById(int id)
        {
            return ctx.Books
                .AsNoTracking()
                .Include(x => x.AuthorsLink)
                .ThenInclude(x => x.Author)
                .Include(x => x.BookRental)
                .SingleOrDefault(x => x.BookId == id);
        }

        public void Update(Book entity)
        {
            ctx.Update(entity);
            ctx.SaveChanges();
        } 
    }
}

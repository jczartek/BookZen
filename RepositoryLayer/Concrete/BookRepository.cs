﻿using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
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

        public List<Book> GetAll()
        {
            return ctx.Books
                      .Include(x => x.AuthorsLink)
                          .ThenInclude(x => x.Author)
                      .Include(x => x.BookRental)
                      .ToList();
        }

        public Book GetBookByIsbn(string isbn)
        {
            return ctx.Books
                .Include(x => x.AuthorsLink)
                .ThenInclude(x => x.Author)
                .Include(x => x.BookRental)
                .SingleOrDefault(x => x.Isbn == isbn);
        }

        public Book GetById(int id)
        {
            return ctx.Books
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

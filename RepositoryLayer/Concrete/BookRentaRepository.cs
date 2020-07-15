using DataLayer.Entities;
using RepositoryLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Concrete
{
    public class BookRentaRepository : Repository, IBookRentalRepository
    {
        public void Add(BookRental entity)
        {
            ctx.Add(entity);
            ctx.SaveChanges();
        }

        public void Delete(BookRental entity)
        {
            ctx.Remove(entity);
            ctx.SaveChanges();
        }

        public List<BookRental> GetAll()
        {
            return ctx.BookRentals
                .ToList();
        }

        public BookRental GetByBookId(int bookId)
        {
            return ctx.BookRentals
                .SingleOrDefault(x => x.BookId == bookId);
        }

        public BookRental GetById(int id)
        {
            return ctx.BookRentals
                .SingleOrDefault(x => x.BookRentalId == id);
        }

        public void Update(BookRental entity)
        {
            ctx.Update(entity);
            ctx.SaveChanges();
        }
    }
}

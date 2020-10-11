using DataLayer.Entities;
using RepositoryLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Concrete
{
    public class BookRentaRepository : Repository, IBookRentalRepository
    {
        public void Add(Borrower entity)
        {
            ctx.Add(entity);
            ctx.SaveChanges();
        }

        public void Delete(Borrower entity)
        {
            ctx.Remove(entity);
            ctx.SaveChanges();
        }

        public List<Borrower> GetAll()
        {
            return ctx.Borrowers
                .ToList();
        }

        public Borrower GetByBookId(int bookId)
        {
            throw new NotImplementedException();
        }

        public Borrower GetById(int id)
        {
            return ctx.Borrowers
                .SingleOrDefault(x => x.BorrowerId == id);
        }

        public void Update(Borrower entity)
        {
            ctx.Update(entity);
            ctx.SaveChanges();
        }
    }
}

using DataLayer.Entities;
using RepositoryLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Concrete
{
    public class BookRentalRepository : Repository, IBookRentalRepository
    {
        public void Add(BookRental entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(BookRental entity)
        {
            throw new NotImplementedException();
        }

        public List<BookRental> GetAll()
        {
            throw new NotImplementedException();
        }

        public BookRental GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(BookRental entity)
        {
            throw new NotImplementedException();
        }
    }
}

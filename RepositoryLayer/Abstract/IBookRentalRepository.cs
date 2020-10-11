using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Abstract
{
    public interface IBookRentalRepository : IRepository<Borrower>
    {
        Borrower GetByBookId(int bookId);
    }
}

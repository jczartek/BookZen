﻿using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Abstract
{
    public interface IBookRepository : IRepository<Book>
    {
        Book GetBookByIsbn(string isbn);
        void DeleteAllBookAuthorsByBookId(int bookId);
        void DeleteBookRentalByBookId(int bookId);
    }
}

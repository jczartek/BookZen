using DataLayer.Entities;
using System;
using System.Collections.Generic;

namespace ServiceLayer.Abstract
{
    public interface IBookRentalService : IDisposable
    {
        void AddBookRental(BookRental bookRental);
        void UpdateBookRental(BookRental bookRental);
        void DeleteBookRentalById(int bookRentalId);
        void DeleteBookRental(BookRental bookRental);
        List<BookRental> GetAllBookRentals();
        BookRental GetBookRentalById(int bookRentalId);
        BookRental GetBookRentalByBookId(int bookId);
    }
}

using DataLayer.Entities;
using RepositoryLayer.Abstract;
using ServiceLayer.Abstract;
using System;
using System.Collections.Generic;

namespace ServiceLayer.Concrete
{
    public class BookRentalService : BaseService<BookRental>, IBookRentalService
    {
        public BookRentalService(IBookRentalRepository repository) : base(repository) { }
        
        public void AddBookRental(BookRental bookRental)
        {
            Repository.Add(bookRental);
        }

        public void DeleteBookRental(BookRental bookRental)
        {
            Repository.Delete(bookRental);
        }

        public void DeleteBookRentalById(int bookRentalId)
        {
            var bookRental = GetBookRentalById(bookRentalId);

            if (bookRental is BookRental) Repository.Delete(bookRental);
        }

        public List<BookRental> GetAllBookRentals()
        {
            return Repository.GetAll();
        }

        public BookRental GetBookRentalByBookId(int bookId)
        {
            return (Repository as IBookRentalRepository).GetByBookId(bookId);
        }

        public BookRental GetBookRentalById(int bookRentalId)
        {
            return Repository.GetById(bookRentalId);
        }

        public void UpdateBookRental(BookRental bookRental)
        {
            Repository.Update(bookRental);
        }
    }
}

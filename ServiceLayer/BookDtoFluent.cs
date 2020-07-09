using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace ServiceLayer
{
    public class BookDtoFluent
    {
        private readonly BookDto dto;

        public static BookDtoFluent Create()
        {
            return new BookDtoFluent();
        }

        private BookDtoFluent() { dto = new BookDto(); }

        public BookDto Get() => dto;

        public BookDtoFluent Id(int id)
        {
            dto.BookId = id;
            return this;
        }

        public BookDtoFluent Title(string title)
        {
            if (String.IsNullOrEmpty(title))
                throw new ArgumentException("Argument title can't be empty or null");

            dto.Title = title;
            return this;
        }

        public BookDtoFluent Authors(string authors)
        {
            if (String.IsNullOrEmpty(authors))
                throw new ArgumentException("Argument authors can't be empty or null");

            dto.Authors = authors;
            return this;
        }

        public BookDtoFluent Publisher(string publisher)
        {
            dto.Publisher = publisher;
            return this;
        }

        public BookDtoFluent Isbn(string isbn)
        {
            dto.Isbn = isbn;
            return this;
        }

        public BookDtoFluent Description(string description)
        {
            dto.Description = description;
            return this;
        }

        public BookDtoFluent YearOfPublication(int? yearOfPublication)
        {
            dto.YearOfPublication = yearOfPublication;
            return this;
        }

        public BookDtoFluent BookIsOnLoan(bool isOnLoan = true)
        {
            dto.IsOnLoan = isOnLoan;
            return this;
        }

        public BookDtoFluent By(string nameBorrower)
        {
            dto.NameOfBorrower = nameBorrower;
            return this;
        }

        public BookDtoFluent In(DateTime dateBorrowing)
        {
            dto.DateBorrowing = dateBorrowing;
            return this;
        }

        public BookDtoFluent BookIsRead(bool isRead = true)
        {
            dto.IsRead = isRead;
            return this;
        }

        public BookDtoFluent When(DateTime readDate)
        {
            dto.ReadDate = readDate;
            return this;
        }
    }
}

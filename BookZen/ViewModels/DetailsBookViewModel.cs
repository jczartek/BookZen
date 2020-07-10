﻿using ServiceLayer;

namespace BookZen.ViewModels
{
    class DetailsBookViewModel : ViewModelBase
    {
        private BookDto BookDto;
        public DetailsBookViewModel(BookDto bookDto)
        {
            BookDto = bookDto;
        }

        public string Title
        {
            get => "Title: " + BookDto.Title;
        }

        public string Authors
        {
            get => "Authors: " + BookDto.Authors;
        }

        public string Publisher
        {
            get => "Publisher: " + BookDto.Publisher ?? "N/A";
        }

        public string Isbn
        {
            get => "Isbn: " + BookDto.Isbn ?? "N/A";
        }

        public string Description
        {
            get => "Descripton: " + BookDto.Description ?? "N/A";
        }

        public string YearOfPublication
        {
            get => "Year of publication: " + BookDto.YearOfPublication.ToString();
        }

        public string ReadBook
        {
            get
            {
                var date = BookDto.ReadDate.HasValue ? BookDto.ReadDate.Value.ToString("dd-MM-yyyy") : null;
                var message = BookDto.IsRead
                    ? $"Book was read on {date}."
                    : "The book has not been read yet.";
                return message;
            }
        }

        public string Loan
        {
            get
            {
                var message = BookDto.IsOnLoan
                    ? $"The book was borrowed by {BookDto.NameOfBorrower} on {BookDto.DateBorrowing}."
                    : "The book is not borrowed.";
                return message;
            }
        }
    }
}

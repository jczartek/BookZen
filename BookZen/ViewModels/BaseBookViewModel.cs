using ServiceLayer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace BookZen.ViewModels
{
    public class BaseBookViewModel : ViewModelBase
    {
        #region Private Fields
        private string _Title;
        private string _Authors;
        private string _Description;
        private string _Publisher;
        private int? _YearOfPublication;
        private string _Isbn;
        private bool _IsRead;
        private DateTime? _ReadDate = DateTime.Now;
        private bool _IsOnLoan;
        private string _NameOfBorrower;
        private DateTime _DateBorrowing = DateTime.Now;
        #endregion

        #region Public Fields
        public virtual string Title
        {
            get => _Title;
            set
            {
                if (value != _Title)
                {
                    _Title = value;
                    RaisePropertiesChanged(nameof(Title));
                }
            }
        }

        public virtual string Authors
        {
            get => _Authors;
            set
            {
                if (value != _Authors)
                {
                    _Authors = value;
                    RaisePropertiesChanged(nameof(Authors));
                }
            }
        }

        public virtual string Description
        {
            get => _Description;
            set
            {
                if (value != _Description)
                {
                    _Description = value;
                    RaisePropertiesChanged(nameof(Description));
                }
            }
        }

        public virtual string Publisher
        {
            get => _Publisher;
            set
            {
                if (value != _Publisher)
                {
                    _Publisher = value;
                    RaisePropertiesChanged(nameof(Publisher));
                }
            }
        }

        public virtual string Isbn
        {
            get => _Isbn;
            set
            {
                if (value != _Isbn)
                {
                    _Isbn = value;
                    RaisePropertiesChanged(nameof(Isbn));
                }
            }
        }

        public virtual int? YearOfPublication
        {
            get => _YearOfPublication;
            set
            {
                if (value != _YearOfPublication)
                {
                    _YearOfPublication = value;
                    RaisePropertiesChanged(nameof(YearOfPublication));
                }
            }
        }

        public virtual bool IsRead
        {
            get => _IsRead;
            set
            {
                if (value != _IsRead)
                {
                    _IsRead = value;
                    RaisePropertiesChanged(nameof(IsRead));
                }
            }
        }

        public virtual DateTime? ReadDate
        {
            get => _ReadDate;
            set
            {
                if (value != _ReadDate)
                {
                    _ReadDate = value;
                    RaisePropertiesChanged(nameof(ReadDate));
                }
            }
        }

        public virtual bool IsOnLoan
        {
            get => _IsOnLoan;
            set
            {
                if (value != _IsOnLoan)
                {
                    _IsOnLoan = value;
                    RaisePropertiesChanged(nameof(IsOnLoan));
                }
            }
        }

        public virtual string NameOfBorrower
        {
            get => _NameOfBorrower;
            set
            {
                if (value != _NameOfBorrower)
                {
                    _NameOfBorrower = value;
                    RaisePropertiesChanged(nameof(NameOfBorrower));
                }
            }
        }

        public virtual DateTime DateBorrowing
        {
            get => _DateBorrowing;
            set
            {
                if (value != _DateBorrowing)
                {
                    _DateBorrowing = value;
                    RaisePropertiesChanged(nameof(DateBorrowing));
                }
            }
        }

        protected int BookId { get; set; }
        #endregion

        public BaseBookViewModel(BookDto bookDto)
        {
            if (bookDto != null)
            {
                BookId = bookDto.BookId;
                Title = bookDto.Title;
                Authors = bookDto.Authors;
                Description = bookDto.Description;
                Publisher = bookDto.Publisher;
                YearOfPublication = bookDto.YearOfPublication;
                Isbn = bookDto.Isbn;
                IsRead = bookDto.IsRead;
                ReadDate = bookDto.ReadDate;
                IsOnLoan = bookDto.IsOnLoan;
                NameOfBorrower = bookDto.NameOfBorrower;
                DateBorrowing = bookDto.DateBorrowing;
            }
        }
    }
}

using BookZen.Dialogs;
using DataLayer.Entities;
using ServiceLayer.BookServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace BookZen.ViewModels
{
    public class BookDialogViewModel : ViewModelBase
    {
        public BookDialogViewModel(BookDto bookDto)
        {
            if (bookDto != null)
            {
                _BookId = bookDto.BookId;
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
        #region Private Fields
        private int _BookId;
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
        public string Title
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

        public string Authors
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

        public string Description
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

        public string Publisher
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

        public string Isbn
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

        public int? YearOfPublication
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

        public bool IsRead
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

        public DateTime? ReadDate
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

        public bool IsOnLoan
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

        public string NameOfBorrower
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

        public DateTime DateBorrowing
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
        #endregion

        #region Commands
        private ICommand _SaveCommand;
        public ICommand SaveCommand
        {
            get => _SaveCommand ??= new RelayCommand(
                (o) => 
                {
                    if (o is InputBookDialog dialog)
                    {
                        BookService.Init()
                            .Id(_BookId)
                            .Title(Title)
                            .Authors(Authors)
                            .Description(Description)
                            .Publisher(Publisher)
                            .Isbn(Isbn)
                            .YearOfPublication(YearOfPublication)
                            .IsBookRead(IsRead, (dto) => { dto.IsRead = IsRead; dto.ReadDate = ReadDate; })
                            .IsBookOnLoan(IsOnLoan, (dto) => { dto.IsOnLoan = IsOnLoan; dto.NameOfBorrower = NameOfBorrower; dto.DateBorrowing = DateBorrowing; })
                            .SaveToDatabase();

                        dialog.DialogResult = true;
                        dialog.Close();
                    }
                });
        }
        #endregion
    }
}

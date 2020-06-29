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
    public class BookDialogViewModel : BaseBookViewModel
    {
        public BookDialogViewModel(BookDto bookDto) : base(bookDto) { }

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
                            .Id(BookId)
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

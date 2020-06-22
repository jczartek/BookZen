using BookZen.Dialogs;
using ServiceLayer.BookServices;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace BookZen.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public List<BookDto> Books
        {
            get => BookService.GetAllBooks();
        }

        private ICommand addBookCommand;
        public ICommand AddBookCommand
        {
            get
            {

                return addBookCommand ??= new RelayCommand(
                    (parameter) =>
                    {
                        new InputBookDialog().ShowDialog();
                        RaisePropertiesChanged(nameof(Books));
                    });
            }
        }

        private ICommand showDetailsCommand;
        public ICommand ShowDetailsCommand
        {
            get
            {
                return showDetailsCommand ??= new RelayCommand(
                    (parameter) =>
                    {
                        throw new NotImplementedException();
                    });
            }
        }

        private ICommand updateBookCommand;
        public ICommand UpdateBookCommand
        {
            get
            {
                return updateBookCommand ??= new RelayCommand(
                    (parameter) =>
                    {
                        throw new NotImplementedException();
                    });
            }
        }

        private ICommand deleteBookCommand;
        public ICommand DeleteBookCommand
        {
            get
            {
                return deleteBookCommand ??= new RelayCommand(
                    (parameter) =>
                    {
                        throw new NotImplementedException();
                    });
            }
        }
    }
}

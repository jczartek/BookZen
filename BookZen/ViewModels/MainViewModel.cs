using BookZen.Dialogs;
using ServiceLayer.BookServices;
using System;
using System.Collections.Generic;
using System.Windows;
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
                        new InputBookDialog(null).ShowDialog();
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
                        new DetailsBookDialog(BookService.FindBookById((int)parameter)).ShowDialog();
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
                        new InputBookDialog(BookService.FindBookById((int)parameter)).ShowDialog();
                        RaisePropertiesChanged(nameof(Books));
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
                        MessageBoxResult result = MessageBox.Show("Do you want to delete the book?",
                            "Confirm deleting book",
                            MessageBoxButton.YesNo,
                            MessageBoxImage.Warning);

                        switch(result)
                        {
                            case MessageBoxResult.Yes:
                                BookService.DeleteBook((int)parameter);
                                RaisePropertiesChanged(nameof(Books));
                                break;
                            case MessageBoxResult.No:
                                break;
                        }
                    });
            }
        }
    }
}

using BookZen.Dialogs;
using Microsoft.Win32;
using ServiceLayer;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace BookZen.ViewModels
{
    public class MainViewModel : ViewModelBase
    {

        public MainViewModel()
        {
            ServiceFactory.CreateBookService(async service => { Books = await service.GetAllBooksAsync(); });
        }

        private List<BookDto> books;
        public List<BookDto> Books
        {
            get
            {
                return books;
            }
            
            set
            {
                books = value;
                RaisePropertiesChanged(nameof(Books));
            }
        }

        private bool editMode;
        public bool EditMode
        {
            get => editMode;
            set
            {
                editMode = value;
                RaisePropertiesChanged(nameof(EditMode));
            }
        }

        private ICommand switchEditModeCommand;
        public ICommand SwitchEditModeCommand
        {
            get => switchEditModeCommand ??= new RelayCommand(
                (parameter) =>
                {
                    EditMode = (bool)parameter;
                });
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
                        var bookDto = ServiceFactory.CreateBookService((service, id) => service.GetBookById(id), (int)parameter);
                        //new DetailsBookDialog(bookDto).ShowDialog();
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
                        var bookDto = ServiceFactory.CreateBookService((service, id) => service.GetBookById(id), (int)parameter);
                        //new InputBookDialog(bookDto).ShowDialog();
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
                                ServiceFactory.CreateBookService((service, id) => service.DeleteBookById(id), (int)parameter);
                                RaisePropertiesChanged(nameof(Books));
                                break;
                            case MessageBoxResult.No:
                                break;
                        }
                    });
            }
        }

        private ICommand exportBooks;
        public ICommand ExportBooks
        {
            get
            {
                return exportBooks ??= new RelayCommand(
                    (_) =>
                    {
                        SaveFileDialog saveFileDialog = new SaveFileDialog
                        {
                            FileName = $"{DateTime.Now.ToString("yyyyMMddHHmmssffff")}-bookzen-backup",
                            DefaultExt = "json",
                            Filter = "Json files (*.json)|*.json|All files (*.*)|*.*",
                            Title = "Export books..."
                        };
                        if (saveFileDialog.ShowDialog().Value)
                        {

                        }
                    });
            }
        }
    }
}

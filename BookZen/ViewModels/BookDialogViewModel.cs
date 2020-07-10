using BookZen.Dialogs;
using ServiceLayer;
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
                        ServiceFactory.CreateBookService(service =>
                        {
                            var dto = BookDtoFluent
                            .Create()
                            .Id(BookId)
                            .Title(Title)
                            .Authors(Authors)
                            .Description(Description)
                            .Publisher(Publisher)
                            .Isbn(Isbn)
                            .YearOfPublication(YearOfPublication)
                            .BookIsRead(IsRead).When(ReadDate)
                            .BookIsOnLoan(IsOnLoan).By(NameOfBorrower).In(DateBorrowing)
                            .Get();

                            if (BookId == 0) service.AddBook(dto);
                            else service.UpdateBook(dto);
                        });

                        dialog.DialogResult = true;
                        dialog.Close();
                    }
                });
        }
        #endregion
    }
}

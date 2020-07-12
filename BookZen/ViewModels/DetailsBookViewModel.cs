using ServiceLayer;

namespace BookZen.ViewModels
{
    class DetailsBookViewModel : BaseBookViewModel
    {
        public DetailsBookViewModel(BookDto bookDto) : base(bookDto) { }

        public string ReadBook
        {
            get
            {
                var date = ReadDate.HasValue ? ReadDate.Value.ToString("dd-MM-yyyy") : null;
                var message = IsRead
                    ? $"Book was read on {date}."
                    : "The book has not been read yet.";
                return message;
            }
        }

        public string Loan
        {
            get
            {
                var message = IsOnLoan
                    ? $"The book was borrowed by {NameOfBorrower} on {DateBorrowing}."
                    : "The book is not borrowed.";
                return message;
            }
        }
    }
}

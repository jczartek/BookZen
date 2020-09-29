using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceLayer
{
    public class BookDto
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? YearOfPublication { get; set; }
        public string Publisher { get; set; }
        public string Isbn { get; set; }
        public bool IsRead { get; set; }
        public DateTime? ReadDate { get; set; }

        public string Authors { get; set; }
        
        public bool IsOnLoan { get; set; }
        public string NameOfBorrower { get; set; }
        public DateTime DateBorrowing { get; set; }
    }
}

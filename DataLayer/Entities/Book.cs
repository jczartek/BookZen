using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.Entities
{
    public class Book
    {
        public int BookId { get; set; }
        
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public string Publisher { get; set; }
        public int? YearOfPublication { get; set; }
        public string Isbn { get; set; }
        public DateTime? ReadDate { get; set; }

        // Relationships
        public int? BorrowerId { get; set; }
        public Borrower Borrower { get; set; }
        public ICollection<BookAuthor> AuthorsLink { get; set; }
    }
}

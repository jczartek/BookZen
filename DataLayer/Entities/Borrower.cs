using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Entities
{
    public class Borrower
    {
        public int BorrowerId { get; set; }

        [Required]
        public string Name { get; set; }
        public DateTime DateBorrowing { get; set; }

        // Relationships
        public ICollection<Book> Books { get; set; }
    }
}

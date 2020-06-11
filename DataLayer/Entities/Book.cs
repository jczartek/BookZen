using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Entities
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Publisher { get; set; }
        public DateTime DateOfPublication { get; set; }
        public string Isbn { get; set; }

        // Relationships
        public BookRental BookRental { get; set; }
        public ICollection<BookAuthor> AuthorsLink { get; set; }
    }
}

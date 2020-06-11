using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DataLayer.Entities
{
    public class BookRental
    {
        public int BookRentalId { get; set; }
        public string Name { get; set; }
        public DateTime DateBorrowing { get; set; }

        // Relationships
        public int BookId { get; set; }
    }
}

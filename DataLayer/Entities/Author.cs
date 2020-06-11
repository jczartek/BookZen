using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Entities
{
    public class Author
    {
        public int AuthorId { get; set; }
        public string Name { get; set; }

        // Relationships
        public ICollection<BookAuthor> BooksLink { get; set; }
    }
}

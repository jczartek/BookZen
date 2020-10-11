using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Entities
{
    public class Author
    {
        public int AuthorId { get; set; }
        
        [Required]
        public string Name { get; set; }

        // Relationships
        public ICollection<BookAuthor> BooksLink { get; set; }
    }
}

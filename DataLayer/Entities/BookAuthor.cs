namespace DataLayer.Entities
{
    public class BookAuthor
    {
        public int BookId { get; set; }
        public int AuthorId { get; set; }

        // Relationships
        public Book Book { get; set; }
        public Author Author { get; set; }
    }
}

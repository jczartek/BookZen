using RepositoryLayer.Abstract;
using RepositoryLayer.Concrete;

namespace RepositoryLayer
{
    public static class RepositoryFactory
    {
        public static IBookRepository CreateBookRepository()
        {
            return new BookRepository();
        }

        public static IAuthorRepository CreateAuthorRepository()
        {
            return new AuthorRepository();
        }

        public static IBookRentalRepository CreateBookRentalRepository()
        {
            return new BookRentaRepository();
        }
    }
}

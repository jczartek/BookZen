using DataLayer;
using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Abstract;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class BookService : BaseService<Book>, IBookService
    {
        public BookService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
        
        public async void AddBook(BookDto dto)
        {
            if (dto != null)
            {
                var book = Mapping.Mapper().Map<Book>(dto);
                //Repository.Add(book);
                UnitOfWork.Repository<Book>().Add(book);
                await UnitOfWork.Commit();
                //dto.BookId = book.BookId;
            }
        }

        public async void DeleteBookById(int id)
        {
            var book = await UnitOfWork.Repository<Book>().Get(id);

            if (book != null) UnitOfWork.Repository<Book>().Delete(book);
            await UnitOfWork.Commit();
        }

        public List<BookDto> GetAllBooks()
        {
            return UnitOfWork
                .Repository<Book>()
                .GetAll(include: source => source
                   .Include(i => i.AuthorsLink)
                   .ThenInclude(i => i.Author))
                .Select(x => Mapping.Mapper().Map<BookDto>(x))
                .ToList();
        }

        public async Task<List<BookDto>> GetAllBooksAsync()
        {
            var books = await UnitOfWork.Repository<Book>()
                .GetAllAsync(include: source => source
                   .Include(i => i.AuthorsLink)
                   .ThenInclude(i => i.Author));

            return await Task.Run(() => books.Select(x => Mapping.Mapper().Map<BookDto>(x)).ToList());
        }

        public async Task<BookDto> GetBookById(int id)
        {
            var book = await UnitOfWork.Repository<Book>().Get(id);

            if (book != null)
                return await Task.FromResult(Mapping.Mapper().Map<BookDto>(book));

            return null;
        }

        public BookDto GetBookByIsbn(string isbn)
        {
            /*var book = (Repository as IBookRepository).GetBookByIsbn(isbn);

            if (book != null)
                return Mapping.Mapper().Map<BookDto>(book);
            */
            return null;
        }

        public void UpdateBook(BookDto dto)
        {
            if (dto != null)
            {
                var book = Mapping.Mapper().Map<Book>(dto);
                UnitOfWork.Repository<Book>().Update(book);
                UnitOfWork.Commit();
            }
        }
    }
}

using NUnit.Framework;
using ServiceLayer;
using ServiceLayer.BookServices;
using System;

namespace NUnitTestBookZen
{
    public class BookServiceTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void AddNewBookWithOneAuthorTest()
        {

            var dto = BookService.FindBookByIsbn("978-83-283-2547-0");

            if (dto == null)
            {
                dto = BookService.Init()
                         .Title("DDD dla architektów oprogramowania")
                         .Authors("Vaughn Vernon")
                         .Publisher("Helion")
                         .Isbn("978-83-283-2547-0")
                         .Description("Niniejsza ksi¹¿ka jest przeznaczona dla architektów aplikacji skali korporacyjnej. Zawarto w niej wyczerpuj¹cy opis narzêdzie DDD.")
                         .YearOfPublication(2016)
                         .SaveToDatabase();
            }

            Assert.AreEqual(dto.Title, "DDD dla architektów oprogramowania");
            Assert.AreEqual(dto.Authors, "Vaughn Vernon");

            dto = BookService.FindBookByIsbn("978-83-283-4279-8");

            if (dto == null)
            {
                dto = BookService.Init()
                         .Title("DDD. Kompendium wiedzy")
                         .Authors("Vaughn Vernon")
                         .Publisher("Helion")
                         .Isbn("978-83-283-4279-8")
                         .YearOfPublication(2018)
                         .Description("To zwiêz³y i czytelnie napisany podrêcznik, który jest przeznaczony dla programistów.")
                         .SaveToDatabase();
            }

            Assert.AreEqual(dto.Title, "DDD. Kompendium wiedzy");
            Assert.AreEqual(dto.Authors, "Vaughn Vernon");
        }

        [Test]
        public void UpdateBook()
        {
            var dto = BookService.Init()
                 .Title("Ksi¹¿ka do aktualizacji")
                 .Authors("Nieznani")
                 .SaveToDatabase();

            Assert.Greater(dto.BookId, 0);

            int bookId = dto.BookId;
            dto.Title = "Tytu³ Siê zmieni³";

            BookService.UpdateBook(dto);

            dto = BookService.FindBookById(bookId);

            Assert.AreEqual("Tytu³ Siê zmieni³", dto.Title);

            dto.Authors = "Kowalski Jan, Kowalski Micha³";

            BookService.UpdateBook(dto);

            dto = BookService.FindBookById(bookId);

            Assert.AreEqual("Kowalski Jan, Kowalski Micha³", dto.Authors);

            dto = BookService.FindBookById(bookId);

            BookService.DeleteBook(dto.BookId);

        }

        [Test]
        public void TestBookRental()
        {

            var dto = ServiceFactory.CreateBookService(service =>
            {
                var dto = BookDtoFluent
                .Create()
                .Title("Po¿yczona ksi¹¿ka")
                .Authors("Kowalski Jan")
                .BookIsOnLoan().By("Kowalski Micha³").In(DateTime.Now)
                .Get();

                service.AddBook(dto);
                return dto;
            });

            Assert.IsTrue(dto.IsOnLoan);
            Assert.AreEqual("Kowalski Micha³", dto.NameOfBorrower);
            Assert.AreEqual(DateTime.Now.Day, dto.DateBorrowing.Day);
            Assert.AreEqual(DateTime.Now.Month, dto.DateBorrowing.Month);
            Assert.AreEqual(DateTime.Now.Year, dto.DateBorrowing.Year);

            dto = ServiceFactory.CreateBookService((service, id) =>
            {
                var bookDto = service.GetBookById(id);
                bookDto.NameOfBorrower = "Kowalska Ewa";
                bookDto.DateBorrowing = new DateTime(2019, 01, 01);

                service.UpdateBook(bookDto);
                return service.GetBookById(id);
            }, dto.BookId);

            Assert.IsTrue(dto.IsOnLoan);
            Assert.AreEqual("Kowalska Ewa", dto.NameOfBorrower);
            Assert.AreEqual(new DateTime(2019, 01, 01), dto.DateBorrowing);

            dto = ServiceFactory.CreateBookService((service, id) =>
            {
                var bookDto = service.GetBookById(id);
                bookDto.IsOnLoan = false;

                service.UpdateBook(bookDto);
                return service.GetBookById(id);
            }, dto.BookId);

            Assert.IsFalse(dto.IsOnLoan);

            ServiceFactory.CreateBookService((service, id) => { service.DeleteBookById(id); }, dto.BookId);
        }
    }
}
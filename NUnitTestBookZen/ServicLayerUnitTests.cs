using DataLayer.Entities;
using NUnit.Framework;
using ServiceLayer;
using System;

namespace NUnitTestBookZen
{
    
    public class BookServiceTest
    {
        [OneTimeSetUp]
        public void Setup()
        {
            Mapping.Init();
        }

        [Test]
        public void TestAddBook()
        {
            var dto = ServiceFactory.CreateBookService(service =>
            {
                var bookDto = service.GetBookByIsbn("978-83-283-2547-0");

                if (bookDto != null)
                {
                    bookDto =
                    BookDtoFluent.Create()
                    .Title("DDD dla architekt�w oprogramowania")
                    .Authors("Vaughn Vernon")
                    .Publisher("Helion")
                    .Isbn("978-83-283-2547-0")
                    .Description("Niniejsza ksi��ka jest przeznaczona dla architekt�w aplikacji skali korporacyjnej. Zawarto w niej wyczerpuj�cy opis narz�dzie DDD.")
                    .YearOfPublication(2016)
                    .Get();
                }
                service.AddBook(bookDto);
                return bookDto;
            });
            Assert.AreEqual(dto.Title, "DDD dla architekt�w oprogramowania");
            Assert.AreEqual(dto.Authors, "Vaughn Vernon");
            ServiceFactory.CreateBookService((service, id) => { service.DeleteBookById(id); }, dto.BookId);
        }

        [Test]
        public void TestUpdateBook()
        {
            var dto = ServiceFactory.CreateBookService(service =>
            {
                var bookDto = BookDtoFluent
                .Create()
                .Title("Ksi��ka do aktualizacji")
                .Authors("Nieznani")
                .Get();

                service.AddBook(bookDto);
                return bookDto;
            });
            Assert.Greater(dto.BookId, 0);

            dto = ServiceFactory.CreateBookService((service, id) =>
            {
                var bookDto = service.GetBookById(id);
                bookDto.Title = "Tytu� Si� zmieni�";

                service.UpdateBook(bookDto);
                return bookDto;
            }, dto.BookId);
            Assert.AreEqual("Tytu� Si� zmieni�", dto.Title);

            dto = ServiceFactory.CreateBookService((service, id) =>
            {
                var bookDto = service.GetBookById(id);
                bookDto.Authors = "Kowalski Jan, Kowalski Micha�";

                service.UpdateBook(bookDto);
                return bookDto;
            }, dto.BookId);
            Assert.AreEqual("Kowalski Jan, Kowalski Micha�", dto.Authors);

            ServiceFactory.CreateBookService((service, id) => { service.DeleteBookById(id); }, dto.BookId);
        }

        [Test]
        public void TestBookRental()
        {

            var dto = ServiceFactory.CreateBookService(service =>
            {
                var dto = BookDtoFluent
                .Create()
                .Title("Po�yczona ksi��ka")
                .Authors("Kowalski Jan")
                .BookIsOnLoan().By("Kowalski Micha�").In(new DateTime(2020, 12, 01))
                .Get();

                service.AddBook(dto);
                return dto;
            });

            Assert.IsTrue(dto.IsOnLoan);
            Assert.AreEqual("Kowalski Micha�", dto.NameOfBorrower);
            Assert.AreEqual(new DateTime(2020, 12, 01), dto.DateBorrowing);

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
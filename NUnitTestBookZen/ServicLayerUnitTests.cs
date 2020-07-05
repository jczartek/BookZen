using DataLayer.Entities;
using NUnit.Framework;
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
                         .Title("DDD dla architekt�w oprogramowania")
                         .Authors("Vaughn Vernon")
                         .Publisher("Helion")
                         .Isbn("978-83-283-2547-0")
                         .Description("Niniejsza ksi��ka jest przeznaczona dla architekt�w aplikacji skali korporacyjnej. Zawarto w niej wyczerpuj�cy opis narz�dzie DDD.")
                         .YearOfPublication(2016)
                         .SaveToDatabase();
            }

            Assert.AreEqual(dto.Title, "DDD dla architekt�w oprogramowania");
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
                         .Description("To zwi�z�y i czytelnie napisany podr�cznik, kt�ry jest przeznaczony dla programist�w.")
                         .SaveToDatabase();
            }

            Assert.AreEqual(dto.Title, "DDD. Kompendium wiedzy");
            Assert.AreEqual(dto.Authors, "Vaughn Vernon");
        }

        [Test]
        public void UpdateBook()
        {
            var dto = BookService.Init()
                 .Title("Ksi��ka do aktualizacji")
                 .Authors("Nieznani")
                 .SaveToDatabase();

            Assert.Greater(dto.BookId, 0);

            int bookId = dto.BookId;
            dto.Title = "Tytu� Si� zmieni�";

            BookService.UpdateBook(dto);

            dto = BookService.FindBookById(bookId);

            Assert.AreEqual("Tytu� Si� zmieni�", dto.Title);

            dto.Authors = "Kowalski Jan, Kowalski Micha�";

            BookService.UpdateBook(dto);

            dto = BookService.FindBookById(bookId);

            Assert.AreEqual("Kowalski Jan, Kowalski Micha�", dto.Authors);

            dto = BookService.FindBookById(bookId);

            BookService.DeleteBook(dto.BookId);

        }

        [Test]
        public void TestBookRental()
        {
            var dto = BookService.Init()
                .Title("Po�yczona ksi��ka")
                .Authors("Kowalski Jan")
                .IsBookOnLoan(true, x => { x.IsOnLoan = true; x.NameOfBorrower = "Kowalski Micha�"; x.DateBorrowing = DateTime.Now; })
                .SaveToDatabase();

            dto = BookService.FindBookById(dto.BookId);

            Assert.IsTrue(dto.IsOnLoan);
            Assert.AreEqual("Kowalski Micha�", dto.NameOfBorrower);
            Assert.AreEqual(DateTime.Now.Day, dto.DateBorrowing.Day);
            Assert.AreEqual(DateTime.Now.Month, dto.DateBorrowing.Month);
            Assert.AreEqual(DateTime.Now.Year, dto.DateBorrowing.Year);

            dto.NameOfBorrower = "Kowalska Ewa";
            dto.DateBorrowing = new DateTime(2019, 01, 01);
            BookService.UpdateBook(dto);

            dto = BookService.FindBookById(dto.BookId);
            Assert.IsTrue(dto.IsOnLoan);
            Assert.AreEqual("Kowalska Ewa", dto.NameOfBorrower);
            Assert.AreEqual(new DateTime(2019, 01, 01), dto.DateBorrowing);

            dto.IsOnLoan = false;
            BookService.UpdateBook(dto);

            dto = BookService.FindBookById(dto.BookId);
            Assert.IsFalse(dto.IsOnLoan);

            BookService.DeleteBook(dto.BookId);
        }
    }
}
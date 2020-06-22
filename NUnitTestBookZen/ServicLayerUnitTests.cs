using NUnit.Framework;
using ServiceLayer.BookServices;

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
    }
}
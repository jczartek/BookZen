using Xunit;
using Moq;
using DataLayer;
using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System;
using MockQueryable.Moq;
using ServiceLayer.Specifications;

namespace TestsLayer
{
    public class FilterSpecTest
    {
        private readonly List<Book> books = new List<Book>()
        {
            new Book
            {
                BookId = 1,
                Title = "Patterns of Enterprise Application Architecture",
                ReadDate = DateTime.Now,
            },
            new Book
            {
                BookId = 2,
                Title = "xUnit Test Patterns: Refactoring Test Code",
            },
            new Book
            {
                BookId = 3,
                Title = "Beyond Software Architecture: Creating and Sustaining Winning Solutions",
                BorrowerId = 1,
                Borrower = new Borrower {BorrowerId = 1, Name = "Jan Kowalski"}
            }
        };

        private Mock<DbContext> GetDbContext<TEntity>(IQueryable<TEntity> queryable) where TEntity : class
        {
            var dbSet = queryable.BuildMockDbSet();
            var ctx = new Mock<DbContext>();
            ctx.Setup(x => x.Set<TEntity>()).Returns(dbSet.Object);

            return ctx;
        }    

        [Fact]
        public async void GetAllBooks()
        {
            var ctx = GetDbContext(books.AsQueryable());
            
            using var uow = new UnitOfWork(ctx.Object);
            var repository = uow.Repository<Book>();

            var b = await repository.GetAllAsync();

            Assert.Equal(3, b.Count());
        }

        [Fact]
        public async void GetAllReadBooks()
        {
            var ctx = GetDbContext(books.AsQueryable());

            using var uow = new UnitOfWork(ctx.Object);
            var repository = uow.Repository<Book>();

            var b = await repository.GetAllAsync(new ReadBookFilterSpec());

            Assert.Single(b);
            Assert.Equal(1, b.First().BookId);
        }

        [Fact]
        public async void GetAllBorrowedBooks()
        {
            var ctx = GetDbContext(books.AsQueryable());

            using var uow = new UnitOfWork(ctx.Object);
            var repository = uow.Repository<Book>();

            var b = await repository.GetAllAsync(new BorrowedBooksFilter());

            Assert.Single(b);
            Assert.Equal(3, b.First().BookId);
            Assert.Equal("Jan Kowalski", b.First().Borrower.Name);
        }

        [Fact]
        public async void GetAllBooksAreReadOrBorrowed()
        {
            var ctx = GetDbContext(books.AsQueryable());

            using var uow = new UnitOfWork(ctx.Object);
            var repository = uow.Repository<Book>();

            var b = await repository.GetAllAsync((new BorrowedBooksFilter() | new ReadBookFilterSpec()));

            Assert.Equal(2, b.Count());
            Assert.Equal(1, b.First().BookId);
            Assert.Equal(3, b.Last().BookId);
        }
        
        [Fact]
        public async void GetAllBooksAreNotReadOrBorrowed()
        {
            var ctx = GetDbContext(books.AsQueryable());

            using var uow = new UnitOfWork(ctx.Object);
            var repository = uow.Repository<Book>();

            var spec = !(new BorrowedBooksFilter() | new ReadBookFilterSpec());

            var b = await repository.GetAllAsync(spec);

            Assert.Single(b);
            Assert.Equal(2, b.First().BookId);
        }
    }
}

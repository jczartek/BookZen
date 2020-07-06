using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Concrete
{
    public class AuthorRepository : Repository, IAuthorRepository
    {
        public void Add(Author entity)
        {
            ctx.Add(entity);
            ctx.SaveChanges();
        }

        public void Delete(Author entity)
        {
            ctx.Remove(entity);
            ctx.SaveChanges();
        }

        public List<Author> GetAll()
        {
            return ctx.Authors
                .Include(x => x.BooksLink)
                .ThenInclude(x => x.Book)
                .ToList();
        }

        public Author GetById(int id)
        {
            return ctx.Authors
                .Include(x => x.BooksLink)
                .ThenInclude(x => x.Book)
                .Single(x => x.AuthorId == id);
        }

        public void Update(Author entity)
        {
            ctx.Update(entity);
            ctx.SaveChanges();
        }
    }
}

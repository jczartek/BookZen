using DataLayer.Entities;
using RepositoryLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Concrete
{
    public class AuthorRepository : Repository, IAuthorRepository
    {
        public void Add(Author entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Author entity)
        {
            throw new NotImplementedException();
        }

        public List<Author> GetAll()
        {
            throw new NotImplementedException();
        }

        public Author GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Author entity)
        {
            throw new NotImplementedException();
        }
    }
}

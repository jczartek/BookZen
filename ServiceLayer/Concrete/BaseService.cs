using RepositoryLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer.Concrete
{
     internal class BaseService<TEntity> : IDisposable
    {
        protected IRepository<TEntity> Repository { get; set; }

        protected BaseService(IRepository<TEntity> repository)
        {
            this.Repository = repository;
        }

        public void Dispose()
        {
            Repository.Dispose();
        }
    }
}

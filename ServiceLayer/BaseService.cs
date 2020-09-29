using RepositoryLayer.Abstract;
using System;

namespace ServiceLayer.Concrete
{
    public class BaseService<TEntity> : IDisposable
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

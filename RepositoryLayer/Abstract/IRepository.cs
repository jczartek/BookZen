using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Abstract
{
    public interface IRepository<TEntity> : IDisposable
    {
        TEntity GetById(int id);
        List<TEntity> GetAll();
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}

using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> Get(int id);
        List<TEntity> GetAll(FilterSpecification<TEntity> spec = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null);

        Task<List<TEntity>> GetAllAsync(FilterSpecification<TEntity> spec = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null);

        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}

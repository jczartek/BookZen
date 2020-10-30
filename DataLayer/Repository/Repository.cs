using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataLayer.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext _ctx;
        public Repository(DbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<TEntity> Get(int id)
        {
            return await _ctx.Set<TEntity>().FindAsync(id);
        }

        public List<TEntity> GetAll(FilterSpecification<TEntity> spec = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {
            IQueryable<TEntity> query = _ctx.Set<TEntity>();

            if (include != null)
            {
                query = include(query);
            }

            if (spec != null)
            {
                query = query.Where(spec);
            }

            return query.ToList();
        }
        public void Add(TEntity entity)
        {
            _ctx.Set<TEntity>().Add(entity);
        }

        public void Update(TEntity entity)
        {
            _ctx.Set<TEntity>().Update(entity);
        }

        public void Delete(TEntity entity)
        {
            _ctx.Set<TEntity>().Remove(entity);
        }

        public async Task<List<TEntity>> GetAllAsync(FilterSpecification<TEntity> spec = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {
            IQueryable<TEntity> query = _ctx.Set<TEntity>();

            if (include != null)
            {
                query = include(query);
            }

            if (spec != null)
            {
                query = query.Where(spec);
            }

            return await query.AsNoTracking().ToListAsync();
        }
    }
}

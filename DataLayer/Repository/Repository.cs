using Microsoft.EntityFrameworkCore;
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

        public async Task<IReadOnlyList<TEntity>> GetAll(FilterSpecification<TEntity> spec = null)
        {
            IQueryable<TEntity> query = _ctx.Set<TEntity>();

            if (spec != null)
            {
                query = query.Where(spec.Specification);
            }

            return await query.ToListAsync();
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
    }
}

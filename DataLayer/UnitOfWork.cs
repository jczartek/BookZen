using DataLayer.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Threading.Tasks;

namespace DataLayer
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _ctx;
        private Hashtable _repositories;

        public UnitOfWork(DbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<int> Commit()
        {
            return await _ctx.SaveChangesAsync();
        }

        public void Dispose()
        {
            _ctx.Dispose();
        }

        public IRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            if (_repositories == null)
                _repositories = new Hashtable();

            var type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(Repository<>);

                var repositoryInstance =
                    Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _ctx);

                _repositories.Add(type, repositoryInstance);
            }

            return (IRepository<TEntity>)_repositories[type];
        }

    }
}

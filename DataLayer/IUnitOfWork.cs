using System;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> Commit();
        IRepository<TEntity> Repository<TEntity>() where TEntity : class;
    }
}
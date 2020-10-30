using DataLayer;
using RepositoryLayer.Abstract;
using System;

namespace ServiceLayer
{
    public class BaseService<TEntity> : IDisposable
    {
        //protected RepositoryLayer.Abstract.IRepository<TEntity> Repository { get; set; }
        protected IUnitOfWork UnitOfWork { get; set; }

        protected BaseService(IUnitOfWork unitOfWork)
        {
            this.UnitOfWork = unitOfWork;
        }

        public void Dispose()
        {
            //Repository.Dispose();
            //UnitOfWork.Dispose();
        }
    }
}

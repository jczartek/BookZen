using DataLayer;
using System;

namespace ServiceLayer
{
    public class BaseService<TEntity> : IDisposable
    {
        private bool disposed;

        protected IUnitOfWork UnitOfWork { get; set; }

        protected BaseService(IUnitOfWork unitOfWork)
        {
            this.UnitOfWork = unitOfWork;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed) return;

            if (disposing) UnitOfWork.Dispose();

            disposed = true;
        }
    }
}

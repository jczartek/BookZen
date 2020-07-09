using DataLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Concrete
{
    public abstract class Repository : IDisposable
    {
        protected DbCoreContext ctx = DbCoreContextFactory.Create();

        public void Dispose()
        {
            ctx.Dispose();
        }
    }
}

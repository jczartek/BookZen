using DataLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Concrete
{
    public abstract class Repository
    {
        protected DbCoreContext ctx = DbCoreContextFactory.Create();
    }
}

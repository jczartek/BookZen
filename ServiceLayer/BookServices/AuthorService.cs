using DataLayer;
using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceLayer.BookServices
{
    static class AuthorService
    {
        public static Author FindAuthorByName(string name)
        {
            using (var ctx = DbCoreContextFactory.Create())
            {
                return ctx.Authors.SingleOrDefault(a => a.Name == name);
            }
        }
    }
}

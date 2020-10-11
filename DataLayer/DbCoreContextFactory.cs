using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer
{
    public class DbCoreContextFactory : IDesignTimeDbContextFactory<DbCoreContext>
    {
        private static string ConnectionString = @"Server=DESKTOP-UJL5D3G\SQLEXPRESS;Database=DBBookZen;Trusted_Connection=True;MultipleActiveResultSets=true";
        public DbCoreContext CreateDbContext(string[] args)
        {
            var options = new DbContextOptionsBuilder<DbCoreContext>();
            options.UseSqlServer(ConnectionString, b => b.MigrationsAssembly("DataLayer"));

            return new DbCoreContext(options.Options);
        }

        public static DbCoreContext Create(string connectionString = null)

        {
            if (connectionString != null)
                ConnectionString = connectionString;

            return new DbCoreContextFactory().CreateDbContext(null);
        }
    }
}

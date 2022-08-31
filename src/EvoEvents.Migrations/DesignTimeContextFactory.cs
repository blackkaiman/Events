using EvoEvents.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Linq;

namespace EvoEvents.Migrations
{
    public class DesignTimeContextFactory : IDesignTimeDbContextFactory<EvoEventsContext>
    {
        private const string LocalSql = "server=localhost;database=EvoEvents-Local;Trusted_Connection=True;";

        private static readonly string MigrationAssemblyName = typeof(DesignTimeContextFactory).Assembly.GetName().Name;

        public EvoEventsContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<EvoEventsContext>()
                .UseSqlServer(args.FirstOrDefault() ?? LocalSql,
                op => op.MigrationsAssembly(MigrationAssemblyName));
            return new EvoEventsContext(builder.Options);
        }
    }
}
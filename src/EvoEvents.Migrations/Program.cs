using Microsoft.EntityFrameworkCore;

namespace EvoEvents.Migrations
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var context = new DesignTimeContextFactory().CreateDbContext(args);
            context.Database.Migrate();
        }
    }
}

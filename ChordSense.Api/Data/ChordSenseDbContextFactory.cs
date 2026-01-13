using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ChordSense.Api.Data
{
    public class ChordSenseDbContextFactory : IDesignTimeDbContextFactory<ChordSenseDbContext>
    {
        public ChordSenseDbContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<ChordSenseDbContext>();
            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));

            return new ChordSenseDbContext(optionsBuilder.Options);
        }
    }
}
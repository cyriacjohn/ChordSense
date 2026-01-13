using Microsoft.EntityFrameworkCore;
using ChordSense.Api.Models;


namespace ChordSense.Api.Data
{
    public class ChordSenseDbContext : DbContext
    {
        public ChordSenseDbContext(DbContextOptions<ChordSenseDbContext> options) : base(options) 
        {
        }

        public DbSet<Song> Songs { get; set; }
    }
}

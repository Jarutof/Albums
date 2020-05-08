using Microsoft.EntityFrameworkCore;

namespace Albums.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Album> Albums { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=local.sqlite");
        }

    }
}

using Microsoft.EntityFrameworkCore;

namespace AutoAPI.Model
{
    public class DbEngineContext : DbContext
    {
        public DbEngineContext(DbContextOptions<DbEngineContext> options)
        : base(options)
        { }

        public DbSet<Auto> automobili { get; set; }

        public DbSet<Acquirente> acquirente { get; set; }

        public DbSet<Credenziali> credenziali { get; set; }

    }
}

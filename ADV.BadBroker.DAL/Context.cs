using Microsoft.EntityFrameworkCore;

namespace ADV.BadBroker.DAL;

public class Context : DbContext
{
    public Context(DbContextOptions<Context> options) : base(options)
    {        
        Database.EnsureCreated();
    }

    public DbSet<UserExtradition> UserExtradition { get; set; }

    public DbSet<Settings> Settings { get; set; }

    public DbSet<User> Users { get; set; }

    public DbSet<SettlementAccount> SettlementAccount { get; set; }

    public DbSet<CurrencyReference> CurrencyReference { get; set; }
}

using Microsoft.EntityFrameworkCore;

namespace ADV.BadBroker.DAL;

public class Context : DbContext
{
    public Context(DbContextOptions<Context> options) : base(options)
    {
        throw new ArgumentNullException("No connection options DB");
    }

    public virtual DbSet<UserExtradition> UserExtradition { get; set; }

    public virtual DbSet<Settings> Settings { get; set; }
}

using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace We.Ping.Smart.EntityFrameworkCore;

[ConnectionStringName(SmartDbProperties.ConnectionStringName)]
public class SmartDbContext : AbpDbContext<SmartDbContext>, ISmartDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * public DbSet<Question> Questions { get; set; }
     */

    public SmartDbContext(DbContextOptions<SmartDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureSmart();
    }
}

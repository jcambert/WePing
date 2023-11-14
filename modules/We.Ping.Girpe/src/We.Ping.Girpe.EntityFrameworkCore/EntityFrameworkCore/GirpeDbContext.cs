using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace We.Ping.Girpe.EntityFrameworkCore;

[ConnectionStringName(GirpeDbProperties.ConnectionStringName)]
public class GirpeDbContext : AbpDbContext<GirpeDbContext>, IGirpeDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * public DbSet<Question> Questions { get; set; }
     */

    public GirpeDbContext(DbContextOptions<GirpeDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureGirpe();
    }
}

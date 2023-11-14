using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace We.Ping.Girpe.EntityFrameworkCore;

[ConnectionStringName(GirpeDbProperties.ConnectionStringName)]
public interface IGirpeDbContext : IEfCoreDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */
}

using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace We.Ping.Smart.EntityFrameworkCore;

[ConnectionStringName(SmartDbProperties.ConnectionStringName)]
public interface ISmartDbContext : IEfCoreDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */
}

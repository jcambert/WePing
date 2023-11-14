using We.Ping.Girpe.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace We.Ping.Girpe;

/* Domain tests are configured to use the EF Core provider.
 * You can switch to MongoDB, however your domain tests should be
 * database independent anyway.
 */
[DependsOn(
    typeof(GirpeEntityFrameworkCoreTestModule)
    )]
public class GirpeDomainTestModule : AbpModule
{

}

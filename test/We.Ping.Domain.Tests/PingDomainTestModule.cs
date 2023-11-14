using We.Ping.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace We.Ping;

[DependsOn(
    typeof(PingEntityFrameworkCoreTestModule)
    )]
public class PingDomainTestModule : AbpModule
{

}

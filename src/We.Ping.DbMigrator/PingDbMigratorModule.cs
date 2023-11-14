using We.Ping.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace We.Ping.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(PingEntityFrameworkCoreModule),
    typeof(PingApplicationContractsModule)
    )]
public class PingDbMigratorModule : AbpModule
{
}

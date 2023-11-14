using Volo.Abp.Modularity;

namespace We.Ping;

[DependsOn(
    typeof(PingApplicationModule),
    typeof(PingDomainTestModule)
    )]
public class PingApplicationTestModule : AbpModule
{

}

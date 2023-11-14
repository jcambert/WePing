using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace We.Ping.Girpe;

[DependsOn(
    typeof(GirpeDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
    )]
public class GirpeApplicationContractsModule : AbpModule
{

}

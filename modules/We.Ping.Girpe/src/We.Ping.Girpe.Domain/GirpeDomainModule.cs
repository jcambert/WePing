using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace We.Ping.Girpe;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(GirpeDomainSharedModule)
)]
public class GirpeDomainModule : AbpModule
{

}

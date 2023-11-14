using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace We.Ping.Smart;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(SmartDomainSharedModule)
)]
public class SmartDomainModule : AbpModule
{

}

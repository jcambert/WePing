using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace We.Ping.Smart;

[DependsOn(
    typeof(SmartDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
    )]
public class SmartApplicationContractsModule : AbpModule
{

}

using Volo.Abp.Modularity;

namespace We.Ping.Smart;

[DependsOn(
    typeof(SmartApplicationModule),
    typeof(SmartDomainTestModule)
    )]
public class SmartApplicationTestModule : AbpModule
{

}

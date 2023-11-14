using Volo.Abp.Modularity;

namespace We.Ping.Girpe;

[DependsOn(
    typeof(GirpeApplicationModule),
    typeof(GirpeDomainTestModule)
    )]
public class GirpeApplicationTestModule : AbpModule
{

}

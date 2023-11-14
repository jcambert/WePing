using Volo.Abp.Autofac;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace We.Ping.Smart;

/// <summary>
/// 
/// </summary>
[DependsOn(
    typeof(AbpAutofacModule),
    typeof(SmartHttpApiClientModule),
    typeof(AbpHttpClientIdentityModelModule)
    )]
public class SmartConsoleApiClientModule : AbpModule
{

}

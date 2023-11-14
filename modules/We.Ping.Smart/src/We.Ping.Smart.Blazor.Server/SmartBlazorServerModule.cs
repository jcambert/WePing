using Volo.Abp.AspNetCore.Components.Server.Theming;
using Volo.Abp.Modularity;

namespace We.Ping.Smart.Blazor.Server;

[DependsOn(
    typeof(AbpAspNetCoreComponentsServerThemingModule),
    typeof(SmartBlazorModule)
    )]
public class SmartBlazorServerModule : AbpModule
{

}

using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace We.Ping.Girpe;

[DependsOn(
    typeof(GirpeApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class GirpeHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(GirpeApplicationContractsModule).Assembly,
            GirpeRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<GirpeHttpApiClientModule>();
        });

    }
}

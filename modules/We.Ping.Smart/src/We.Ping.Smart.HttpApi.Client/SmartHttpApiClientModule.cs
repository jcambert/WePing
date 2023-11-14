using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace We.Ping.Smart;

[DependsOn(
    typeof(SmartApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class SmartHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(SmartApplicationContractsModule).Assembly,
            SmartRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<SmartHttpApiClientModule>();
        });

    }
}

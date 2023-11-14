using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace We.Ping.Smart;

[DependsOn(
    typeof(AbpVirtualFileSystemModule)
    )]
public class SmartInstallerModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<SmartInstallerModule>();
        });
    }
}

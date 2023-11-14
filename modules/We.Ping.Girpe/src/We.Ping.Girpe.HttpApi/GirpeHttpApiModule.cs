using Localization.Resources.AbpUi;
using We.Ping.Girpe.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace We.Ping.Girpe;

[DependsOn(
    typeof(GirpeApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class GirpeHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(GirpeHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<GirpeResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}

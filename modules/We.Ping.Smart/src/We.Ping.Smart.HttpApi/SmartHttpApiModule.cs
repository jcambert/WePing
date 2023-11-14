using Localization.Resources.AbpUi;
using We.Ping.Smart.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace We.Ping.Smart;

[DependsOn(
    typeof(SmartApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class SmartHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(SmartHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<SmartResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}

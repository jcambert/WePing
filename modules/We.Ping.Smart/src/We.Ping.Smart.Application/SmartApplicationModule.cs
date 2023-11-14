using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Application;
using Microsoft.Extensions.Configuration;

namespace We.Ping.Smart;

[DependsOn(
    typeof(SmartDomainModule),
    typeof(SmartApplicationContractsModule),
    typeof(AbpDddApplicationModule),
    typeof(AbpAutoMapperModule)
    )]
[DependsOn(
    typeof(PingResquestModule)
    )]
public class SmartApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddTransient(typeof(IDeserializeService<>),typeof(DeserializeService<>));
        context.Services.AddAutoMapperObjectMapper<SmartApplicationModule>();
        ConfigureOptions(context);  
    }

    private void ConfigureOptions(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();
        context.Services.Configure<UrlOptions>(
            options => configuration.GetSection("SmartUrl").Bind(options)
        );
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<SmartApplicationModule>(validate: true);
        });
    }
}

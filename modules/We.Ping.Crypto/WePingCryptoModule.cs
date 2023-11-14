using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace We.Ping.Crypto;

/// <summary>
///
/// </summary>
public class WePingCryptoModule : AbpModule
{
    /// <inheritdoc/>
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        base.ConfigureServices(context);
        //context.Services.AddTransient<ITokenGeneratorService, TokenGeneratorService>();
        ConfigureOptions(context);
    }

    private void ConfigureOptions(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();
        context.Services.Configure<SpidAuthOptions>(
            options => configuration.GetSection("SpidAuth").Bind(options)
        );
    }
}

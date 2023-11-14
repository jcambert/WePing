using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RestSharp;
using RestSharp.Serializers;
using Volo.Abp.Modularity;
using We.Ping.Crypto;

namespace We.Ping.Request;

/// <summary>
///
/// </summary>
[DependsOn(typeof(WePingCryptoModule))]
public class PingResquestModule : AbpModule
{
    /// <inheritdoc/>
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        base.ConfigureServices(context);

        //context.Services.AddSingleton<IInternalPingRequestService, InternalPingRequestService>();
        ConfigureRequestOptions(context);
        ConfigureHttpClient(context);
    }

    private void ConfigureRequestOptions(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();
        context.Services.Configure<RequestSettings>(
            options => configuration.GetSection("SmartRequest").Bind(options)
        );
    }

    private void ConfigureHttpClient(ServiceConfigurationContext context)
    {
        var policies = new RequestPolicies();
        context.Services
            .AddHttpClient("pingrequest")
            .ConfigureHttpClient(
                (sp, client) =>
                {
                    var opt =
                        sp.GetService<IOptions<RequestSettings>>()?.Value ?? new RequestSettings();
                    client.BaseAddress = new Uri(opt.BaseUrl);
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    //client.Timeout = TimeSpan.FromSeconds(Math.Pow(2, DEFAULT_RETRY_COUNT) + 1);
                }
            )
            .SetHandlerLifetime(TimeSpan.FromSeconds(10));
        //.AddPolicyHandler(GetRetryPolicy())
        //.AddPolicyHandlerFromRegistry("WaitAndRetryPolicy");



        var policyRegistry = context.Services.AddPolicyRegistry(
            (sp, e) =>
            {
                var logger = sp.GetService<ILogger<IHttpClientFactory>>();

#pragma warning disable CS8604 // Existence possible d'un argument de référence null.
                e[RequestPolicies.WaitAndRetryPolicy] = RequestPolicies.GetWaitAndRetryPolicy(
                    logFunction: logger.LogTrace
                );
#pragma warning restore CS8604 // Existence possible d'un argument de référence null.
            }
        );
        //policyRegistry.Add("WaitAndRetryPolicy", GetRetryPolicy());

        context.Services.AddSingleton(
            services =>
            {
                var factory = services.GetService<IHttpClientFactory>();
                var httpClient = factory?.CreateClient("pingrequest");
#pragma warning disable CS8604 // Existence possible d'un argument de référence null.
                var restClient = new RestClient(
                    httpClient,
                    configureSerialization: s => s.UseXml()
                );
#pragma warning restore CS8604 // Existence possible d'un argument de référence null.


                return restClient;
            }
        );
    }
}

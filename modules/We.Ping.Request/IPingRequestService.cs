using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Registry;
using RestSharp;
using Volo.Abp.DependencyInjection;
using We.AbpExtensions;
using We.Results;

namespace We.Ping.Request;

public class RequestQueryHandler : AbpHandler.With<RequestQuery, RequestQueryResponse>
{
    private  IInternalPingRequestService _pingRequestService=> LazyServiceProvider.GetRequiredService<IInternalPingRequestService>();


    private ApiUriBuilder _uriBuilder=>LazyServiceProvider.GetRequiredService<ApiUriBuilder>();

    public RequestQueryHandler(IAbpLazyServiceProvider serviceProvider) : base(serviceProvider)
    {
  
        
        
    }

    protected override async Task<Result<RequestQueryResponse>> InternalHandle(
        RequestQuery request,
        CancellationToken cancellationToken
    )
    {
        await base.InternalHandle(request, cancellationToken);

        var uri =await _uriBuilder.Build(request, cancellationToken);
        if (uri)
        {
            Logger.LogInformation($"Send request to:{uri.Value}");
            var response = await _pingRequestService.GetAsync(uri.Value, cancellationToken);
            if (response.IsSuccessful)
                return new RequestQueryResponse(response);
            return Result.Failure<RequestQueryResponse>(response.StatusCode.ToString(), "");
        }
        return Result.Failure<RequestQueryResponse>(uri.Errors.ToArray());
    }

}

/// <summary>
///
/// </summary>
internal interface IInternalPingRequestService
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="url"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<RestResponse> GetAsync(string url, CancellationToken? cancellationToken = null);
}

/// <summary>
///
/// </summary>
[Dependency(ServiceLifetime.Singleton, ReplaceServices = true)]
[ExposeServices(typeof(IInternalPingRequestService))]
internal class InternalPingRequestService : IInternalPingRequestService
{
    private readonly ILogger<IInternalPingRequestService> Logger;
    private readonly RestClient RestClient;
    private readonly IPolicyRegistry<string> PolicyRegistry;
    private readonly IAsyncPolicy<RestResponse> WaitAndRetryPolicy;

    /// <summary>
    ///
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="rest"></param>
    /// <param name="policyRegistry"></param>
    public InternalPingRequestService(
        ILogger<IInternalPingRequestService> logger,
        RestClient rest,
        IPolicyRegistry<string> policyRegistry
    )
    {
        this.Logger = logger;
        this.RestClient = rest;
        this.PolicyRegistry = policyRegistry;
        this.WaitAndRetryPolicy = PolicyRegistry.Get<IAsyncPolicy<RestResponse>>(
            RequestPolicies.WaitAndRetryPolicy
        );
        //logger.LogCritical($"BaseUrl:{rest.Options.BaseUrl}");
    }

    ///<inheritdoc/>
    public async Task<RestResponse> GetAsync(
        string url,
        CancellationToken? cancellationToken = null
    )
    {
        var restResponse = await WaitAndRetryPolicy.ExecuteAsync(
            async () =>
            {
                RestResponse? response = default(RestResponse);
                ;
                try
                {
                    var request = new RestRequest(url);
                    response = await RestClient.GetAsync(
                        request,
                        cancellationToken ?? CancellationToken.None
                    );
                    return response;
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex.Message);
                    response = new RestResponse
                    {
                        Content = ex.Message,
                        ErrorMessage = ex.Message,
                        ResponseStatus = ResponseStatus.TimedOut,
                        StatusCode = System.Net.HttpStatusCode.ServiceUnavailable
                    };
                }
                return response;
            }
        );
        return restResponse;
    }
}

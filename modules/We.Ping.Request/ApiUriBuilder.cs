using Mediator;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Volo.Abp.DependencyInjection;
using We.AbpExtensions;
using We.Ping.Crypto;
using We.Results;

namespace We.Ping.Request;

[Dependency(ServiceLifetime.Singleton, ReplaceServices = true)]
public class ApiUriBuilder
{
    private IAbpLazyServiceProvider LazyServiceProvider { get; init; }
    private RequestSettings _requestOptions => LazyServiceProvider.GetRequiredService<IOptions<RequestSettings>>()?.Value ?? new RequestSettings();
    private SpidAuthOptions _spidAuthOptions => LazyServiceProvider.GetRequiredService<IOptions<SpidAuthOptions>>()?.Value ?? new SpidAuthOptions();
    public IMediator Mediator => LazyServiceProvider.Mediator();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="serviceProvider"></param>
    public ApiUriBuilder(IAbpLazyServiceProvider serviceProvider)
    {
        this.LazyServiceProvider = serviceProvider;       
    }
    private string FormatUriWithParameters(
        string uri,
        SpidAuth auth,
        IDictionary<string, string> opts
    )
    {
        var dico = new Dictionary<string, string>(opts);
        dico["id"] = _spidAuthOptions.AppId;
        dico["serie"] = _spidAuthOptions.Serie;
        dico["tm"] = auth.tm;
        dico["tmc"] = auth.tmc;
        var result = QueryHelpers.AddQueryString(uri, dico);
        return result;
    }

    public async Task<Result<string>> Build(RequestQuery request,CancellationToken cancellationToken)
    {
        var token = await Mediator.Send(new TokenGeneratorQuery(),cancellationToken);
        if (token.IsSuccess)
        {
            var auth = token.Value.Authentication;
            var _requestUrl= !string.IsNullOrEmpty(request.BaseUrl)?$"{request.BaseUrl}{request.Url}":request.Url;
            
            string uri = FormatUriWithParameters($"{_requestUrl}.{_requestOptions.Extension}", auth, request.Parameters);
            return uri;
        }
        return Result.Failure<string>(token.Errors.ToArray());
    }
}

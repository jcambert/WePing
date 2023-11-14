using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.DependencyInjection;
using RestSharp;
using Volo.Abp.DependencyInjection;

namespace We.Ping.Request;

/// <summary>
///
///
/// </summary>
public interface IRequestQuery : WeM.IQuery<RequestQueryResponse>
{
    string BaseUrl { get;set; }
    /// <summary>
    ///
    /// </summary>
    string Url { get; set; }

    /// <summary>
    ///
    /// </summary>
    IDictionary<string, string> Parameters { get; }

    IRequestQuery AddParameters(string key, string value);

    IRequestQuery AddParameters(string key, string value, Func<string, bool> filter);
}

///<inheritdoc/>
[Dependency(ServiceLifetime.Transient, ReplaceServices = true)]
[ExposeServices(typeof(IRequestQuery))]
public sealed class RequestQuery : IRequestQuery
{
    ///<inheritdoc/>
    public string BaseUrl { get; set; } = string.Empty;
    ///<inheritdoc/>
    public string Url { get; set; } = string.Empty;

    /// <summary>
    ///
    /// </summary>
    public IDictionary<string, string> Parameters { get; } = new Dictionary<string, string>();

    public IRequestQuery AddParameters(string key, string value)
    {
        Parameters[key] = value;
        return this;
    }
    public IRequestQuery AddParameters(string key, string value, Func<string, bool> filter)
        => filter(value) ? AddParameters(key, value) : this;

    public override string ToString()
    =>  QueryHelpers.AddQueryString(Url, Parameters);
}

public sealed record RequestQueryResponse(RestResponse Response) : WeM.Response;

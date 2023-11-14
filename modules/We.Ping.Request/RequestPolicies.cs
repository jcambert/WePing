using Polly;
using RestSharp;
using System.Net;
using We.Results;
using SN = System.Net;

namespace We.Ping.Request;

internal class RequestPolicies
{
    private const int DEFAULT_RETRY_COUNT = 5;

    internal const string WaitAndRetryPolicy = "WaitAndRetryPolicy";

    internal static IAsyncPolicy<RestResponse> GetWaitAndRetryPolicy(
        Action<string?, object?[]>? logFunction = null
    )
    {
        var retryPolicy = Policy
            .HandleResult<RestResponse>(x => x.StatusCode != SN.HttpStatusCode.OK)
            .WaitAndRetryAsync(
                DEFAULT_RETRY_COUNT,
                retryAttempt => TimeSpan.FromSeconds(2), // TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                (iRestResponse, timeSpan, retryCount, context) =>
                {
                    logFunction?.Invoke(
                        $"The request failed. HttpStatusCode={iRestResponse.Result.StatusCode}. Waiting {timeSpan} seconds before retry. Number attempt {retryCount}. Uri={iRestResponse.Result.ResponseUri}; RequestResponse={iRestResponse.Result.Content}",
                        Array.Empty<object>()
                    );
                }
            );
        return retryPolicy;

        var circuitBreakerPolicy = Policy
            //.HandleResult<RestResponse>(x => x.StatusCode == HttpStatusCode.ServiceUnavailable)
            .HandleResult<RestResponse>(x => x.StatusCode != SN.HttpStatusCode.OK)
            .CircuitBreakerAsync(
                3,
                TimeSpan.FromSeconds(15),
                onBreak: (iRestResponse, timespan, context) =>
                {
                    logFunction?.Invoke(
                        $"Circuit went into a fault state. Reason: {iRestResponse.Result.Content}",
                        Array.Empty<object>()
                    );
                },
                onReset: (context) =>
                {
                    logFunction?.Invoke($"Circuit left the fault state.", Array.Empty<object>());
                }
            );

        var policy = retryPolicy.WrapAsync(circuitBreakerPolicy);
        return policy;
    }
}

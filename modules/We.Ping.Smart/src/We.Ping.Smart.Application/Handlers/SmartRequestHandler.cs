using Microsoft.Extensions.Options;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Caching;
using Volo.Abp.Domain.Entities;
using We.Mediatr;

namespace We.Ping.Smart.Handlers;

public abstract class SmartRequestHandler<TQuery, TResponse, TEntity, TEntityDto, TXml> : AbpHandler.With<TQuery, TResponse, TEntity, TEntityDto>
    where TQuery :  ISmartQuery<TResponse>
    where TResponse : SmartResponse
    where TEntity : Entity
    where TEntityDto : EntityDto
{
    protected TResponse EmptyResponse { get; init; }
    protected IDeserializeService<TXml>? Deserializer => LazyServiceProvider.GetService<IDeserializeService<TXml>>();
    protected UrlOptions urlOptions => LazyServiceProvider.GetService<IOptions<UrlOptions>>()?.Value ?? new UrlOptions();
    protected RequestSettings requestOptions => LazyServiceProvider.GetService<IOptions<RequestSettings>>()?.Value ?? new RequestSettings();
    protected IDistributedCache<TEntity, string>? DistributedCache => LazyServiceProvider.GetRequiredService<IDistributedCache<TEntity, string>>();
    public SmartRequestCacheService SmartCache => LazyServiceProvider.GetRequiredService<SmartRequestCacheService>();
    protected ApiUriBuilder _uriBuilder => LazyServiceProvider.GetRequiredService<ApiUriBuilder>();
    protected SmartRequestHandler(IAbpLazyServiceProvider serviceProvider, TResponse emptyResponse) : base(serviceProvider)
    {
        this.EmptyResponse = emptyResponse;
    }

    protected virtual async Task<Result<(string,RequestQuery)>> GetFullApiUri(TQuery query, CancellationToken cancellationToken)
    {
        
        var req = GetRequestQuery(query);
        req.BaseUrl = requestOptions.BaseUrl;
        var uriResponse=  await _uriBuilder.Build(req, cancellationToken);
        if (uriResponse)
        {
            return Result.Success<(string, RequestQuery)>( (uriResponse.Value, req));
        }
        return Result.Failure<(string, RequestQuery)>("Error while constructing Api Url");
    }
    protected abstract RequestQuery GetRequestQuery(TQuery query);
    protected abstract Result<TResponse> GetUrlOnlyResponse(string uri);
    protected override async Task<Result<TResponse>> InternalHandle(TQuery request, CancellationToken cancellationToken)
    {


        var uri = await GetFullApiUri(request, cancellationToken);
        if (!uri)
        {
            LogError(uri.Errors.AsString());
            return Result.Failure<TResponse>(uri.Errors.ToArray());
        }


        if (DistributedCache is null)
            LogWarning("Distributed service cache is not registered. skipping load from cache");
        else
        {
            var key = uri.Value.Item2.ToString();
            LogInformation($"Try load from cache for query {typeof(TQuery).Name} with key {key}");
            var r0 = await TryLoadFromCache(key, cancellationToken);
            if (r0)
            {
                LogInformation($"Load {key} from cache");
                if (r0.Value is not null)
                {
                    r0.Value.Uri = uri.Value.Item1;
                    r0.Value.LoadFrom = LoadFrom.Cache;
                }
                else
                    LogWarning($"{typeof(TQuery).Name} Cache result has Null Value");

                return r0;
            }
        }

        LogInformation($"Try to load from database for query {typeof(TQuery).Name}");
        if (await TryLoadFromDatabase(out var r1, request, cancellationToken))
        {
            LogInformation("Loaded from database");
            if (r1.Value is not null)
            {
                r1.Value.Uri = uri.Value.Item1;
                r1.Value.LoadFrom = LoadFrom.Database;

            }
            else
                LogWarning($"{typeof(TQuery).Name} Database result has Null Value");
            return r1;
        }

        var r2 = await LoadFromSpid(request, uri.Value.Item2, cancellationToken);
        if (r2.Value is not null)
        {
            r2.Value.Uri = uri.Value.Item1;
            r2.Value.LoadFrom = LoadFrom.Spid;
        }
        else
            LogWarning($"{typeof(TQuery).Name} Spid result has Null Value");

        return r2;

    }

    protected virtual Task<bool> TryAddToCacheAsync(string key, TResponse res, CancellationToken cancellationToken)
    {
        LogInformation($"Adding to cache for query {typeof(TQuery).Name} is not implemented");
        //result = Result.Failure<TResponse>($"Adding to cache for query {typeof(TQuery).Name} is not implemented");
        return Task.FromResult(false);
    }

    protected virtual Task<Result<TResponse>> TryLoadFromCache(string key, CancellationToken cancellationToken)
    {
        LogInformation($"Loading from cache for query {typeof(TQuery).Name} is not implemented");
        return Task.FromResult(Result.Failure<TResponse>($"Loading from cache for query {typeof(TQuery).Name} is not implemented"));

    }

    protected virtual Task AddToDatabase(TEntity entity)
    {
        return Task.CompletedTask;
    }
    protected virtual Task<bool> TryLoadFromDatabase(out Result<TResponse> result, TQuery request, CancellationToken cancellationToken)
    {
        LogInformation($"Loading from database for query {typeof(TQuery).Name} is not implemented");
        result = Result.Failure<TResponse>($"Loading from database for query {typeof(TQuery).Name} is not implemented");
        return Task.FromResult(false);
    }

    protected virtual async Task<Result<TResponse>> LoadFromSpid(TQuery request, RequestQuery query, CancellationToken cancellationToken)
    {
        if (Deserializer is null)
            return Result.Failure<TResponse>($"Deserializer service for query {typeof(TQuery).Name} is not registered");
        LogInformation($"Loading from SPID for query {typeof(TQuery).Name} ");
        Result<TResponse>? result = default(Result<TResponse>);
        //var query = GetRequestQuery(request);
        await Result
           .Create(query)
           .Bind(q => Mediator.Send(q).AsTaskWrap())
           .OnAsync(
                async response =>
                {
                    try
                    {
                        try
                        {
                            var v = Deserializer?.To(new StringReader(response?.Response?.Content ?? string.Empty));



                            if (v is not null && v)
                            {

                                result = HandleSpidResponse(request, v);
                                if (DistributedCache is null)
                                    LogWarning("Distributed service cache is not registered. skipping caching");
                                else
                                {
                                    string key = query.ToString();
                                    LogInformation($"Try Add To cache with key {key}");
                                    if (!await TryAddToCacheAsync(key, result.Value, cancellationToken))
                                        LogError($"An error happened while addin to cache for query {typeof(TQuery).Name}");

                                }

                            }
                            else
                            {
                                result = Result.Failure<TResponse>(EmptyResponse, v?.Errors.ToArray());
                            }
                        }catch (Exception ex)
                        {
                            LogError(ex.InnerException?.Message);
                            LogError(ex.InnerException?.StackTrace);
                            throw ;
                        }
                    }
                    catch (Exception e)
                    {
                        LogError(e.Message);
                        LogError(e.StackTrace);
                        result = Result.Failure<TResponse>(EmptyResponse, new Error(e));
                    }
                },
                failure =>
                {
                    result = Result.Failure<TResponse>(EmptyResponse, failure.Errors.ToArray());
                }
            );
        return result ?? Result.Failure<TResponse>(EmptyResponse,$"You must ensure that Handler {this.GetType().Name}.{nameof(InternalHandle)} does not return null");
    }
    protected abstract TResponse HandleSpidResponse(TQuery request, Result<TXml> result);


}


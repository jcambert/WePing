using Volo.Abp.Application.Dtos;
using Volo.Abp.Caching;
using Volo.Abp.Domain.Entities;
using Volo.Abp.ObjectMapping;

namespace We.Ping.Smart.Handlers;

[Dependency(ServiceLifetime.Singleton, ReplaceServices = true)]
[ExposeServices(typeof(SmartRequestCacheService))]
public sealed class SmartRequestCacheService
{
    public IAbpLazyServiceProvider ServiceProvider { get; }
    public Type? ObjectMapperContext { get; set; } = null;

    public IObjectMapper ObjectMapper => ServiceProvider.ObjectMapper(ObjectMapperContext);
    public SmartRequestCacheService(IAbpLazyServiceProvider serviceProvider)
    {
        this.ServiceProvider = serviceProvider;
    }



    public async Task<bool> TryAddListToCacheAsync<TResponse, TEntity, TEntityDto>(string key, List<TEntityDto> values, CancellationToken cancellationToken)
        where TEntity : Entity
        where TEntityDto : EntityDto
    {
        var cache = ServiceProvider.GetRequiredService<IDistributedCache<List<TEntity>, string>>();
        var cs = ObjectMapper.Map<List<TEntityDto>, List<TEntity>>(values);

        await cache.SetAsync(key, cs, token: cancellationToken);
        return true;
    }

    public async Task<Result<List<TEntityDto>>> TryLoadListFromCache<TEntity, TEntityDto>(string key, CancellationToken cancellationToken)
        where TEntity : Entity
        where TEntityDto : EntityDto
    {
        var cache = ServiceProvider.GetRequiredService<IDistributedCache<List<TEntity>, string>>();
        var res = await cache.GetAsync(key, token: cancellationToken);
        if (res == null)
            return Result.Failure<List<TEntityDto>>($"Nothing in cache with key {key}");
        var cs = ObjectMapper.Map<List<TEntity>, List<TEntityDto>>(res);
        return Result.Success(new List<TEntityDto>(cs));
    }
}
using Volo.Abp.Caching;
using We.Ping.Smart.Entities;
using We.Ping.Smart.Entities.Clubs;
using We.Ping.Smart.Queries;

namespace We.Ping.Smart.Handlers;

public sealed class BrowseEquipePourClubHandler :
    SmartRequestHandler<BrowseEquipePourClubQuery, BrowseEquipePourClubResponse, EquipePourClub, EquipePourClubDto, ListEquipePourClub>
{
    public BrowseEquipePourClubHandler(IAbpLazyServiceProvider serviceProvider) 
        : base(serviceProvider,new(new()))
    {
    }

    protected override RequestQuery GetRequestQuery(BrowseEquipePourClubQuery query)
    {
        var q = new RequestQuery() { Url = urlOptions.ListeEquipePourClub };
        q.AddParameters("numclu", query.Numero, StringExtensions.IsNotNullNorEmpty);
        q.AddParameters("type", query.Type.ToCode(), StringExtensions.IsNotNullNorEmpty);
        return q;
    }

    protected override Result<BrowseEquipePourClubResponse> GetUrlOnlyResponse(string uri)
    => new BrowseEquipePourClubResponse(new()) { Uri =uri };

    protected override BrowseEquipePourClubResponse HandleSpidResponse(BrowseEquipePourClubQuery query,Result<ListEquipePourClub> result)
    {
        var equipes = MapToDtoList(result.Value.Liste);
        return new BrowseEquipePourClubResponse(equipes);
    }

    protected override async Task<bool> TryAddToCacheAsync(string key, BrowseEquipePourClubResponse res, CancellationToken cancellationToken)
        => await SmartCache.TryAddListToCacheAsync<BrowseEquipePourClubResponse, EquipePourClub, EquipePourClubDto>(key, res.Equipes, cancellationToken);
    /*{
        var cache = LazyServiceProvider.GetRequiredService<IDistributedCache<List<EquipePourClub>, string>>();
        var cs = ObjectMapper.Map<List<EquipePourClubDto>, List<EquipePourClub>>(res.Equipes);

        await cache.SetAsync(key, cs, token: cancellationToken);
        return true;
    }*/
    protected override async Task<Result<BrowseEquipePourClubResponse>> TryLoadFromCache(string key, CancellationToken cancellationToken)
    {
        var r = await SmartCache.TryLoadListFromCache<EquipePourClub, EquipePourClubDto>(key, cancellationToken);
        return r ? new BrowseEquipePourClubResponse(r.Value) { LoadFrom = LoadFrom.Cache } : Result.Failure<BrowseEquipePourClubResponse>(r.Errors.ToArray());
        /*var cache = LazyServiceProvider.GetRequiredService<IDistributedCache<List<EquipePourClub>, string>>();
        var res = await cache.GetAsync(key, token: cancellationToken);
        if (res == null)
            return Result.Failure<BrowseEquipePourClubResponse>($"Nothing in cache with key {key}");
        return Result.Success(new BrowseEquipePourClubResponse(MapToDtoList(res)) { LoadFrom = LoadFrom.Cache });*/
    }
}

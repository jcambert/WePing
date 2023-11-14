using Volo.Abp.Caching;
using Volo.Abp.Domain.Entities.Caching;
using Volo.Abp.Domain.Repositories;
using We.Ping.Smart.Entities;
using We.Ping.Smart.Entities.Clubs;
using We.Ping.Smart.Entities.Organismes;
using We.Ping.Smart.Queries;

namespace We.Ping.Smart.Handlers;
public sealed class BrowseClubPourDepartementHandler
    : SmartRequestHandler<BrowseClubPourDepartementQuery, BrowseClubPourDepartementResponse, ClubPourDepartement, ClubPourDepartementDto, ListClubPourDepartement>
{

    private IEntityCache<ClubPourDepartement, string>? _cache => LazyServiceProvider.GetService<IEntityCache<ClubPourDepartement, string>>();
    private IRepository<ClubPourDepartement>? _repo => LazyServiceProvider.GetService<IRepository<ClubPourDepartement>>();

    public BrowseClubPourDepartementHandler(IAbpLazyServiceProvider serviceProvider)
        : base(serviceProvider, new(new()))
    {


    }

    protected override RequestQuery GetRequestQuery(BrowseClubPourDepartementQuery query)
    {

        var q = new RequestQuery() { Url = urlOptions.ListeClubs };
        q.AddParameters("dep", query.Departement, StringExtensions.IsNotNullNorEmpty);
        q.AddParameters("code", query.CodePostal, StringExtensions.IsNotNullNorEmpty);
        q.AddParameters("ville", query.Ville, StringExtensions.IsNotNullNorEmpty);
        q.AddParameters("numero", query.Numero, StringExtensions.IsNotNullNorEmpty);
        return q;
    }

    protected override BrowseClubPourDepartementResponse HandleSpidResponse(BrowseClubPourDepartementQuery query, Result<ListClubPourDepartement> result)
    {
        var clubs = MapToDtoList(result.Value.Liste);
        return new BrowseClubPourDepartementResponse(clubs) { LoadFrom=LoadFrom.Spid};
    }

    protected override async Task<bool> TryAddToCacheAsync(string key, BrowseClubPourDepartementResponse res, CancellationToken cancellationToken)
        => await SmartCache.TryAddListToCacheAsync<BrowseClubPourDepartementResponse, ClubPourDepartement, ClubPourDepartementDto>(key, res.Clubs, cancellationToken);
    /*{
        var cache = LazyServiceProvider.GetRequiredService<IDistributedCache<List<ClubPourDepartement>, string>>();
        var cs = ObjectMapper.Map<List<ClubPourDepartementDto>, List<ClubPourDepartement>>(res.Clubs);

        await cache.SetAsync(key, cs, token: cancellationToken);
        return true;
    }*/
    protected override async Task<Result<BrowseClubPourDepartementResponse>> TryLoadFromCache(string key, CancellationToken cancellationToken)
    {
        var r = await SmartCache.TryLoadListFromCache<ClubPourDepartement, ClubPourDepartementDto>(key, cancellationToken);
        return r ? new BrowseClubPourDepartementResponse(r.Value) { LoadFrom = LoadFrom.Cache } : Result.Failure<BrowseClubPourDepartementResponse>(r.Errors.ToArray());

      /*  var cache = LazyServiceProvider.GetRequiredService<IDistributedCache<List<ClubPourDepartement>, string>>();
        var res = await cache.GetAsync(key, token: cancellationToken);
        if (res == null)
            return Result.Failure<BrowseClubPourDepartementResponse>($"Nothing in cache with key {key}");
        return Result.Success(new BrowseClubPourDepartementResponse(MapToDtoList(res)) { LoadFrom = LoadFrom.Cache });*/
    }

    protected override Result<BrowseClubPourDepartementResponse> GetUrlOnlyResponse(string uri)
    => new BrowseClubPourDepartementResponse(new()) { Uri = uri };
}


using Volo.Abp.Caching;
using We.Ping.Smart.Entities;
using We.Ping.Smart.Entities.Clubs;
using We.Ping.Smart.Entities.Organismes;
using We.Ping.Smart.Queries;

namespace We.Ping.Smart.Handlers;

public sealed class NombreEquipePourClubHandler :
    SmartRequestHandler<NombreEquipePourClubQuery, NombreEquipePourClubResponse, EquipePourClub, EquipePourClubDto, ListEquipePourClub>
{
    public NombreEquipePourClubHandler(IAbpLazyServiceProvider serviceProvider) 
        : base(serviceProvider, new(new()))
    {
    }

    protected override RequestQuery GetRequestQuery(NombreEquipePourClubQuery query)
    {
        var q = new RequestQuery() { Url = urlOptions.ListeEquipePourClub };
        q.AddParameters("numclu", query.Numero, StringExtensions.IsNotNullNorEmpty);
        q.AddParameters("type", query.Type.ToCode(), StringExtensions.IsNotNullNorEmpty);
        return q;
    }

    protected override Result<NombreEquipePourClubResponse> GetUrlOnlyResponse(string uri)
    => Result.Success(new NombreEquipePourClubResponse(new() {Numero=string.Empty,Nombre=0 }) { Uri = uri });

    protected override NombreEquipePourClubResponse HandleSpidResponse(NombreEquipePourClubQuery query, Result<ListEquipePourClub> result)
    {
        var equipes = MapToDtoList(result.Value.Liste);
        return new NombreEquipePourClubResponse(new() { Numero = query.Numero, Nombre = equipes.Count });
    }

    protected override async Task<bool> TryAddToCacheAsync(string key, NombreEquipePourClubResponse res, CancellationToken cancellationToken)
         => await SmartCache.TryAddListToCacheAsync<NombreEquipePourClubResponse, NombreEquipePourClub, NombreEquipePourClubDto>(key,new List<NombreEquipePourClubDto>() { res.Nombre }, cancellationToken);
    /*{
        var cache = LazyServiceProvider.GetRequiredService<IDistributedCache<NombreEquipePourClub, string>>();

        await cache.SetAsync(key,ObjectMapper.Map<NombreEquipePourClubDto,NombreEquipePourClub>( res.Nombre), token: cancellationToken);
        return true;
    }*/

    protected override async Task<Result<NombreEquipePourClubResponse>> TryLoadFromCache(string key, CancellationToken cancellationToken)
    {
        var r = await SmartCache.TryLoadListFromCache<NombreEquipePourClub, NombreEquipePourClubDto>(key, cancellationToken);
        return r ? new NombreEquipePourClubResponse(r.Value.FirstOrDefault() ?? new() { }) { LoadFrom = LoadFrom.Cache } : Result.Failure<NombreEquipePourClubResponse>(r.Errors.ToArray());
        
       /* var cache = LazyServiceProvider.GetRequiredService<IDistributedCache<NombreEquipePourClub, string>>();
        var res = await cache.GetAsync(key, token: cancellationToken);
        if (res == null)
            return Result.Failure<NombreEquipePourClubResponse>($"Nothing in cache with key {key}");
        return Result.Success(new NombreEquipePourClubResponse(ObjectMapper.Map< NombreEquipePourClub, NombreEquipePourClubDto>(res)) { LoadFrom = LoadFrom.Cache });*/
    }
}

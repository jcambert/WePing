using We.Ping.Smart.Entities;
using We.Ping.Smart.Entities.Organismes;
using We.Ping.Smart.Queries;

namespace We.Ping.Smart.Handlers;

public class BrowseOrganismeHandler : SmartRequestHandler<BrowseOrganismeQuery, BrowseOrganismeResponse, Organisme, OrganismeDto, ListOrganisme>
{
    
    public BrowseOrganismeHandler(IAbpLazyServiceProvider serviceProvider) 
        : base(serviceProvider, new(new()))
    {
    }

    protected override RequestQuery GetRequestQuery(BrowseOrganismeQuery query)
    {
        var q = new RequestQuery() { Url = urlOptions.ListeOrganisme };
        q.AddParameters("type", query.Type, StringExtensions.IsNotNullNorEmpty);
        return q;
    }

    protected override Result<BrowseOrganismeResponse> GetUrlOnlyResponse(string uri)
         => new BrowseOrganismeResponse(new()) { Uri = uri };


    protected override BrowseOrganismeResponse HandleSpidResponse(BrowseOrganismeQuery request, Result<ListOrganisme> result)
     => new BrowseOrganismeResponse(MapToDtoList(result.Value.Liste));


    protected override async Task<bool> TryAddToCacheAsync(string key, BrowseOrganismeResponse res, CancellationToken cancellationToken)
    => await SmartCache.TryAddListToCacheAsync<BrowseOrganismeResponse, Organisme, OrganismeDto>(key, res.Organismes, cancellationToken);

    
    protected override async Task<Result<BrowseOrganismeResponse>> TryLoadFromCache(string key, CancellationToken cancellationToken)
    {
        var r = await SmartCache.TryLoadListFromCache<Organisme, OrganismeDto>(key, cancellationToken);
        return r ? new BrowseOrganismeResponse(r.Value) { LoadFrom = LoadFrom.Cache } : Result.Failure<BrowseOrganismeResponse>(r.Errors.ToArray());
    }
}

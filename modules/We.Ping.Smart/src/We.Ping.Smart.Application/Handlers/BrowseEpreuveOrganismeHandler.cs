using We.Ping.Smart.Entities;
using We.Ping.Smart.Entities.Organismes;
using We.Ping.Smart.Queries;
using We.Results;

namespace We.Ping.Smart.Handlers;

public class BrowseEpreuveOrganismeHandler : SmartRequestHandler<BrowseEpreuveOrganismeQuery, BrowseEpreuveOrganismeResponse, EpreuveOrganisme, EpreuveOrganismeDto, ListeEpreuveOrganisme>
{
    public BrowseEpreuveOrganismeHandler(IAbpLazyServiceProvider serviceProvider) : base(serviceProvider, new(new()))
    {
    }

    protected override RequestQuery GetRequestQuery(BrowseEpreuveOrganismeQuery query)
    {
        var q = new RequestQuery() { Url = urlOptions.EpreuveOrganisme };
        q.AddParameters("organisme", query.IdOrganisme);
        q.AddParameters("type", query.TypeEpreuve);
        return q;
    }

    protected override Result<BrowseEpreuveOrganismeResponse> GetUrlOnlyResponse(string uri)
     => new BrowseEpreuveOrganismeResponse(new()) { Uri = uri };

    protected override BrowseEpreuveOrganismeResponse HandleSpidResponse(BrowseEpreuveOrganismeQuery request, Result<ListeEpreuveOrganisme> result)
    {
        var epreuves = MapToDtoList(result.Value.Epreuves);
        return new BrowseEpreuveOrganismeResponse(epreuves) { LoadFrom = LoadFrom.Spid };
    }
}

using We.Ping.Smart.Entities;
using We.Ping.Smart.Entities.Organismes;
using We.Ping.Smart.Queries;

namespace We.Ping.Smart.Handlers;

public class BrowseDivisionHandler : SmartRequestHandler<BrowseDivisionQuery, BrowseDivisionResponse, Division, DivisionDto, ListDivision>
{
    public BrowseDivisionHandler(IAbpLazyServiceProvider serviceProvider) : base(serviceProvider, new(new()))
    {
    }

    protected override RequestQuery GetRequestQuery(BrowseDivisionQuery query)
    {
        var q = new RequestQuery() { Url = urlOptions.Division };
        q.AddParameters("epreuve", query.IdEpreuve, StringExtensions.IsNotNullNorEmpty);
        q.AddParameters("organisme", query.IdOrganisme, StringExtensions.IsNotNullNorEmpty);
        q.AddParameters("type", query.TypeEpreuve, StringExtensions.IsNotNullNorEmpty);
        return q;
    }

    protected override Result<BrowseDivisionResponse> GetUrlOnlyResponse(string uri)
    => new BrowseDivisionResponse(new()) { Uri = uri };

    protected override BrowseDivisionResponse HandleSpidResponse(BrowseDivisionQuery request, Result<ListDivision> result)
    {
        var divisions = MapToDtoList(result.Value.Divisions);
        return new BrowseDivisionResponse(divisions) { LoadFrom = LoadFrom.Spid };
    }
}

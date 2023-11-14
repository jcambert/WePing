using We.Ping.Smart.Entities;
using We.Ping.Smart.Joueurs;
using We.Ping.Smart.Queries;

namespace We.Ping.Smart.Handlers;

public sealed class BrowseJoueurPartieHandler : SmartRequestHandler<BrowseJoueurPartieQuery, BrowseJoueurPartieResponse, JoueurPartie, JoueurPartieDto, ListeJoueurPartie>
{
    public BrowseJoueurPartieHandler(IAbpLazyServiceProvider serviceProvider) : base(serviceProvider,  new(new()))
    {
    }

    protected override RequestQuery GetRequestQuery(BrowseJoueurPartieQuery query)
    {
        var q = new RequestQuery() { Url = urlOptions.JoueurParties };
        q.AddParameters("numlic", query.Licence);
        return q;
    }

    protected override Result<BrowseJoueurPartieResponse> GetUrlOnlyResponse(string uri)
     => new BrowseJoueurPartieResponse(new()) { Uri = uri };

    protected override BrowseJoueurPartieResponse HandleSpidResponse(BrowseJoueurPartieQuery request, Result<ListeJoueurPartie> result)
    {
        var resulats = MapToDtoList(result.Value.Resultats);
        return new BrowseJoueurPartieResponse(resulats) { LoadFrom = LoadFrom.Spid };
    }
}

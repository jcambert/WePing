using We.Ping.Smart.Entities;
using We.Ping.Smart.Joueurs;
using We.Ping.Smart.Queries;

namespace We.Ping.Smart.Handlers;

public sealed class BrowseJoueurClassementHandler : SmartRequestHandler<BrowseJoueurClassementQuery, BrowseJoueurClassementResponse, JoueurClassement, JoueurClassementDto, ListJoueurClassement>
{
    public BrowseJoueurClassementHandler(IAbpLazyServiceProvider serviceProvider) 
        : base(serviceProvider, new(new()))
    {
    }

    protected override RequestQuery GetRequestQuery(BrowseJoueurClassementQuery query)
    {
        var q = new RequestQuery() { Url = urlOptions.ListeJoueurClassement };
        q.AddParameters("club", query.Club, StringExtensions.IsNotNullNorEmpty);
        q.AddParameters("nom", query.Nom, StringExtensions.IsNotNullNorEmpty);
        q.AddParameters("prenom", query.Prenom, StringExtensions.IsNotNullNorEmpty);
        return q;
    }

    protected override Result<BrowseJoueurClassementResponse> GetUrlOnlyResponse(string uri)
     => new BrowseJoueurClassementResponse(new()) { Uri = uri };

    protected override BrowseJoueurClassementResponse HandleSpidResponse(BrowseJoueurClassementQuery request, Result<ListJoueurClassement> result)
    {
        var joueurs = MapToDtoList(result.Value.Liste);
        return new BrowseJoueurClassementResponse(joueurs) { LoadFrom = LoadFrom.Spid };
    }
}

using We.Ping.Smart.Entities;
using We.Ping.Smart.Joueurs;
using We.Ping.Smart.Queries;

namespace We.Ping.Smart.Handlers;

public sealed class BrowseJoueurLicenceSpidHandler : SmartRequestHandler<BrowseJoueurLicenceSpidQuery, BrowseJoueurLicenceSpidResponse, JoueurLicenceSpid, JoueurLicenceSpidDto, ListJoueurLicenceSpid>
{
    public BrowseJoueurLicenceSpidHandler(IAbpLazyServiceProvider serviceProvider) 
        : base(serviceProvider, new(new()))
    {
    }

    protected override RequestQuery GetRequestQuery(BrowseJoueurLicenceSpidQuery query)
    {
        var q = new RequestQuery() { Url = urlOptions.ListeJoueurLicenceSpid };
        q.AddParameters("club", query.Club, StringExtensions.IsNotNullNorEmpty);
        q.AddParameters("licence", query.Licence, StringExtensions.IsNotNullNorEmpty);
        q.AddParameters("nom", query.Nom, StringExtensions.IsNotNullNorEmpty);
        q.AddParameters("prenom", query.Prenom, StringExtensions.IsNotNullNorEmpty);
        q.AddParameters("valid", ((int)query.ValiditeLicence).ToString());
        return q;
    }

    protected override Result<BrowseJoueurLicenceSpidResponse> GetUrlOnlyResponse(string uri)
     => new BrowseJoueurLicenceSpidResponse(new()) { Uri = uri };

    protected override BrowseJoueurLicenceSpidResponse HandleSpidResponse(BrowseJoueurLicenceSpidQuery request, Result<ListJoueurLicenceSpid> result)
    {
        var joueurs = MapToDtoList(result.Value.ListeJoueurs);
        return new BrowseJoueurLicenceSpidResponse(joueurs) { LoadFrom = LoadFrom.Spid };
    }


}

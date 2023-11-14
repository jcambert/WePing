using We.Ping.Smart.Entities;
using We.Ping.Smart.Joueurs;
using We.Ping.Smart.Queries;

namespace We.Ping.Smart.Handlers;

public class GetJoueurLicenceSpidHandler : SmartRequestHandler<GetJoueurLicenceSpidQuery, GetJoueurLicenceSpidResponse, JoueurLicenceSpid_, JoueurLicenceSpidDto, ListJoueurLicenceSpid>
{
    public GetJoueurLicenceSpidHandler(IAbpLazyServiceProvider serviceProvider) 
        : base(serviceProvider, new(new()))
    {
    }

    protected override RequestQuery GetRequestQuery(GetJoueurLicenceSpidQuery query)
    {
        var q = new RequestQuery() { Url = urlOptions.JoueurLicenceSpid };
        q.AddParameters("licence", query.Licence, StringExtensions.IsNotNullNorEmpty);
        return q;
    }

    protected override Result<GetJoueurLicenceSpidResponse> GetUrlOnlyResponse(string uri)
     => new GetJoueurLicenceSpidResponse(new()) { Uri = uri };


    protected override GetJoueurLicenceSpidResponse HandleSpidResponse(GetJoueurLicenceSpidQuery request, Result<ListJoueurLicenceSpid> result)
    {
        var clubs = MapToDtoList(result.Value.ListeLicences);
        if(clubs.Count==0)
            throw new ApplicationException($"{nameof(GetJoueurLicenceSpidQuery)} spid response return no value");

        if (clubs.Count != 1)
            throw new ApplicationException($"{nameof(GetJoueurLicenceSpidQuery)} spid response return more than 1 result");
        return new GetJoueurLicenceSpidResponse(clubs.First());
    }
}

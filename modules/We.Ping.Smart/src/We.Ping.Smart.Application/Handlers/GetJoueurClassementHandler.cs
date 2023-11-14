using We.Ping.Smart.Entities;
using We.Ping.Smart.Joueurs;
using We.Ping.Smart.Queries;

namespace We.Ping.Smart.Handlers;

public class GetJoueurClassementHandler : SmartRequestHandler<GetJoueurClassementQuery, GetJoueurClassementResponse, JoueurClassement, JoueurClassementDto, ListJoueurClassement>
{
    public GetJoueurClassementHandler(IAbpLazyServiceProvider serviceProvider) 
        : base(serviceProvider, new(new()))
    {
    }

    protected override RequestQuery GetRequestQuery(GetJoueurClassementQuery query)
    {
        var q = new RequestQuery() { Url = urlOptions.JoueurClassement };
        q.AddParameters("licence", query.Licence, StringExtensions.IsNotNullNorEmpty);
        return q;
    }

    protected override Result<GetJoueurClassementResponse> GetUrlOnlyResponse(string uri)
     => new GetJoueurClassementResponse(new()) { Uri = uri };


    protected override GetJoueurClassementResponse HandleSpidResponse(GetJoueurClassementQuery request, Result<ListJoueurClassement> result)
    {
        var clubs = MapToDtoList(result.Value.Liste);
        if(clubs.Count==0)
            throw new ApplicationException($"{nameof(GetJoueurClassementQuery)} spid response return no value");

        if (clubs.Count != 1)
            throw new ApplicationException($"{nameof(GetJoueurClassementQuery)} spid response return more than 1 result");
        return new GetJoueurClassementResponse(clubs.First());
    }
}

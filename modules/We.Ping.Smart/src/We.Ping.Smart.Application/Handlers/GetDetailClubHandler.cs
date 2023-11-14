using We.Ping.Smart.Entities;
using We.Ping.Smart.Entities.Clubs;
using We.Ping.Smart.Queries;

namespace We.Ping.Smart.Handlers;

public sealed class GetDetailClubHandler : SmartRequestHandler<GetDetailClubQuery, GetDetailClubResponse, ClubDetail, ClubDetailDto, ListClubDetail>
{
    public GetDetailClubHandler(IAbpLazyServiceProvider serviceProvider) 
        : base(serviceProvider, new(new()))
    {
    }

    protected override RequestQuery GetRequestQuery(GetDetailClubQuery query)
    {
        var q = new RequestQuery() { Url = urlOptions.DetailClub };
        q.AddParameters("club", query.Numero, StringExtensions.IsNotNullNorEmpty);
        return q;
    }

    protected override Result<GetDetailClubResponse> GetUrlOnlyResponse(string uri)
    => new GetDetailClubResponse(new()) { Uri =uri };

    protected override GetDetailClubResponse HandleSpidResponse(GetDetailClubQuery query,Result<ListClubDetail> result)
    {
        var clubs = MapToDtoList(result.Value.Liste);
        return new GetDetailClubResponse(clubs);
    }
}

using Microsoft.Extensions.Logging;
using We.Ping.Smart.Entities.Clubs;
using We.Ping.Smart.Queries;
using We.Results;

namespace We.Ping.Smart.Blazor.Pages.Smart;

public partial class RechercheClub
{
    private List<ClubPourDepartementDto> Clubs = new();

    public RechercheClub( )
    {
    }

    protected override void Clear()
    {
        base.Clear();
        Clubs.Clear();
        
    }

    public override async Task SendRequest()
    {
        await base.SendRequest();
        Loading(true);
        await Result
             .Create(Query)
             .Bind(q => AppService.BrowseClubPourDepartement(q))
             .OnAsync(
                 res =>
                 {
                     Logger.LogInformation($"{QueryName} succeded");
                     ExternalApiUrl = res.Uri;
                     Clubs.AddRange(res.Clubs);

                 },
                 failure =>
                 {
                     Loading(false);

                     Logger.LogError($"{QueryName} : {failure.Errors.AsString()}");
                     NotificationService?.Error($"{QueryName} : {failure.Errors.AsString()}");
                 },
                 async res =>
                 {

                     foreach (var club in res.Clubs)
                     {
                         await LoadNombreEquipe(club);
                     }
                     Loading(false);

                 }
             );

        

    }

    private async Task LoadNombreEquipe(ClubPourDepartementDto club)
    {
        var q = new NombreEquipePourClubQuery() { Numero = club.Numero };
        await Result
            .Create(q)
            .Bind(q => AppService.NombreEquipePourClub(q))
            .OnAsync(
            res =>
            {
                club.NombreEquipe = res.Nombre.Nombre;
            });
    }

    public async Task LoadDetail(ClubPourDepartementDto club)
    {
        if (club == null)
            return;
        club.IsLoadingDetail = true;
        var query = new GetDetailClubQuery() { Numero = club.Numero };
        await Result
            .Create(query)
            .Bind(q => AppService.GetClubDetail(q))
            .OnAsync(
                res =>
                {
                    club.Details = res.Clubs.FirstOrDefault();
                    club.IsLoadingDetail = false;
                });
    }


}

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using We.Ping.Smart.Joueurs;
using We.Ping.Smart.Queries;
using We.Results;

namespace We.Ping.Smart.Blazor.Pages.Smart;

public partial class RechercheJoueurLicenceSpid
{

    private JoueurLicenceSpidDto? Joueur = null;

    [Parameter] public string Licence { get; set; } = string.Empty;
    public RechercheJoueurLicenceSpid()
    {
    }

    protected override void Clear()
    {
        base.Clear();
        Joueur = null;
    }
    public override async Task SendRequest()
    {
        await base.SendRequest();
        Loading(true);

        await Result
             .Create(Query)
             .Bind(q => AppService.GetJoueurLicenceSpid(q))
             .OnAsync(
                 res =>
                 {
                     Logger.LogInformation($"{QueryName} succeded");
                     ExternalApiUrl = res.Uri;
                     Joueur = res.Joueur;
                     Loading(false);

                 },
                 failure =>
                 {
                     Loading(false);

                     ExternalApiUrl = (failure as Failure<GetJoueurLicenceSpidResponse>).Value?.Uri ?? string.Empty;
                     Logger.LogError($"{QueryName} : {failure.Errors.AsString()}");
                     NotificationService?.Error($"{QueryName} : {failure.Errors.AsString()}");
                 }
             );



    }
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        Query.Licence = Licence;
        if (ValidateQuery())
            await SendRequest();
    }
}

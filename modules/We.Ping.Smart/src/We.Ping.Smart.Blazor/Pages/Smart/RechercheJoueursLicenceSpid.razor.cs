using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Volo.Abp.AspNetCore.Components.Notifications;
using We.Ping.Smart.Joueurs;
using We.Ping.Smart.Queries;
using We.Results;

namespace We.Ping.Smart.Blazor.Pages.Smart;

public partial class RechercheJoueursLicenceSpid
{

    private List<JoueurLicenceSpidDto> Joueurs = new();
    [Parameter] public string Numero { get; set; } = string.Empty;
    public RechercheJoueursLicenceSpid()
    {
    }
    protected override void Clear()
    {
        base.Clear();
        Joueurs.Clear();
    }
    public override async Task SendRequest()
    {
        await base.SendRequest();   
        Loading(true);

        await Result
             .Create(Query)
             .Bind(q => AppService.BrowseJoueurLicenceSpid(q))
             .OnAsync(
                 res =>
                 {
                     Logger.LogInformation($"{QueryName} succeded");
                     ExternalApiUrl = res.Uri;
                     
                     Joueurs.AddRange(res.Joueurs);
                     Loading(false);
                 },
                 failure =>
                 {
                     Loading(false);
                     Logger.LogError($"{QueryName} : {failure.Errors.AsString()}");
                     NotificationService?.Error($"{QueryName} : {failure.Errors.AsString()}");
                 }
             );



    }
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        Query.Club = Numero;
        if (ValidateQuery())
            await SendRequest();
    }


}


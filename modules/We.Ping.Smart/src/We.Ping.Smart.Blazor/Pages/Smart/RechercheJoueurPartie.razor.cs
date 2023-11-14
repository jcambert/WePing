using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using We.Ping.Smart.Joueurs;
using We.Ping.Smart.Queries;
using We.Results;

namespace We.Ping.Smart.Blazor.Pages.Smart;

public partial class RechercheJoueurPartie
{

    private readonly List<JoueurPartieDto> Parties = new();
    [Parameter] public string Numero { get; set; } = string.Empty;

    public RechercheJoueurPartie()
    {
    }
    protected override void Clear()
    {
        base.Clear();
        Parties.Clear();
        ExternalApiUrl = string.Empty;
    }

    public override async Task SendRequest()
    {
        await base.SendRequest();
        Loading(true);

        await Result
             .Create(Query)
             .Bind(q => AppService.BrowseJoueurParties(q))
             .OnAsync(
                 res =>
                 {
                     Logger.LogInformation($"{QueryName} succeded");
                     ExternalApiUrl = res.Uri;
                     Parties.AddRange(res.Parties);
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
        Query.Licence = Numero;
        if (ValidateQuery())
            await SendRequest();
    }
}

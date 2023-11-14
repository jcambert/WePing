using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using We.Ping.Smart.Entities.Organismes;
using We.Ping.Smart.Queries;
using We.Results;

namespace We.Ping.Smart.Blazor.Pages.Smart;

public partial class RechercheDivision
{
    [Parameter] public string? Organisme { get; set; } = string.Empty;
    [Parameter] public string? Epreuve { get; set; } = string.Empty;
    [Parameter] public string? TypeEpreuve { get; set; } = string.Empty;

    public List<DivisionDto> Divisions { get; } = new();

    protected override void Clear()
    {
        base.Clear();
        Divisions.Clear();
    }

    public override async Task SendRequest()
    {
        await base.SendRequest();

        Loading(true);
        await Result
             .Create(Query)
             .Bind(q => AppService.BrowseDivisions(q))
             .OnAsync(
                 res =>
                 {
                     Logger.LogInformation($"{QueryName} succeded");
                     ExternalApiUrl = res.Uri;
                     Divisions.AddRange(res.Divisions);
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
        base.OnInitialized();
        Query.IdOrganisme = Organisme ?? string.Empty;
        Query.IdEpreuve = Epreuve ?? string.Empty;
        Query.TypeEpreuve = TypeEpreuve ?? TypeEpreuveExtensions.TypeEpreuves.First().Value;
        if (ValidateQuery())
            await SendRequest();
    }
}

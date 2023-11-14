using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using static System.Reflection.Metadata.BlobBuilder;
using We.Results;
using We.Ping.Smart.Entities.Organismes;
using System.Runtime.CompilerServices;
using We.Ping.Smart.Queries;

namespace We.Ping.Smart.Blazor.Pages.Smart;

public partial class RechercheEpreuveOrganisme
{
    [Parameter] public string Organisme { get; set; } = string.Empty;
    [Parameter] public string? TypeEpreuve { get; set; } = string.Empty;
    public List<EpreuveOrganismeDto> Epreuves { get; } = new ();

    protected override void Clear()
    {
        base.Clear();
        Epreuves.Clear();
    }

    public override async Task SendRequest()
    {
        await base.SendRequest();

        Loading(true);
        await Result
             .Create(Query)
             .Bind(q => AppService.BrowseEpreuveOrganisme(q))
             .OnAsync(
                 res =>
                 {
                     Logger.LogInformation($"{QueryName} succeded");
                     ExternalApiUrl = res.Uri;
                     Epreuves.AddRange(res.Epreuves);
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
        Query.IdOrganisme = Organisme;
            Query.TypeEpreuve = TypeEpreuve ?? TypeEpreuveExtensions.TypeEpreuves.First().Value;
        if (ValidateQuery())
            await SendRequest();
    }
}

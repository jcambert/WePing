using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using We.Ping.Smart.Entities.Organismes;
using We.Results;

namespace We.Ping.Smart.Blazor.Pages.Smart;

public partial class RechercheOrganisme
{

    private List<OrganismeDto> Organismes = new();
    [Parameter] public string Type { get; set; } = string.Empty;

    public RechercheOrganisme()
    {
    }
    protected override void Clear()
    {
        base.Clear();
        Organismes.Clear();
    }
    public override async Task SendRequest()
    {
        await base.SendRequest();
        Loading(true);
        await Result
                     .Create(Query)
                     .Bind(q => AppService.BrowseOrganismes(q))
                     .OnAsync(
                         res =>
                         {
                             Logger.LogInformation($"{QueryName} succeded");
                             ExternalApiUrl = res.Uri;
                             Organismes.Clear();
                             Organismes.AddRange(res.Organismes);
                             Loading( false);
                         },
                         failure =>
                         {
                             Logger.LogError($"{QueryName} : {failure.Errors.AsString()}");
                             NotificationService?.Error($"{QueryName} : {failure.Errors.AsString()}");
                             Loading(false);
                         }
                     );



    }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        Query.Type = Type;
        if (ValidateQuery())
            await SendRequest();
    }
}

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using We.Ping.Smart.Entities.Clubs;
using We.Results;

namespace We.Ping.Smart.Blazor.Pages.Smart;

public partial class RechercheEquipesClub
{

    
    private List<EquipePourClubDto> Equipes = new();
    [Parameter] public string Numero { get; set; } = string.Empty;
    public RechercheEquipesClub()
    {
    }

    protected override void Clear()
    {
        base.Clear();
        Equipes.Clear();
    }

    public override async Task SendRequest()
    {
        await base.SendRequest();
        Loading(true);
        await Result
                     .Create(Query)
                     .Bind(q => AppService.BrowseEquipePourClub(q))
                     .OnAsync(
                         res =>
                         {
                             Logger.LogInformation($"{QueryName} succeded");
                             ExternalApiUrl = res.Uri;
                             Equipes.AddRange(res.Equipes);
                             Loading(false);

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
        Query.Numero = Numero;
        if (ValidateQuery())
            await SendRequest();
    }
}

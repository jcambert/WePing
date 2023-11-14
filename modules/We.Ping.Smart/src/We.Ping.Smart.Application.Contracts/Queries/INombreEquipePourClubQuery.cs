using FluentValidation;
using We.Ping.Queries;
using We.Ping.Smart.Entities.Clubs;

namespace We.Ping.Smart.Queries;

public interface INombreEquipePourClubQuery:ISmartQuery<NombreEquipePourClubResponse>
{
    string Numero { get; set; }
    TypeEquipePourClub Type { get; set; }
}

[Dependency(ServiceLifetime.Transient, ReplaceServices = true)]
[ExposeServices(typeof(INombreEquipePourClubQuery))]
public sealed class NombreEquipePourClubQuery: SmartQuery,INombreEquipePourClubQuery
{
    public string Numero { get; set; } = string.Empty;
    public TypeEquipePourClub Type { get; set; } = TypeEquipePourClub.ChampionnatFranceMasculin;
}


public sealed record NombreEquipePourClubResponse(NombreEquipePourClubDto Nombre) : SmartResponse;

[Dependency(ServiceLifetime.Singleton, ReplaceServices = true)]
[ExposeServices(typeof(IValidator<NombreEquipePourClubQuery>), typeof(IValidator<INombreEquipePourClubQuery>))]
public sealed class NombreEquipePourClubQueryValidator : AbstractValidator<INombreEquipePourClubQuery>
{
    public NombreEquipePourClubQueryValidator()
    {
        RuleFor(x => x.Numero).NotEmpty().WithMessage("Numero cannot be empty");
    }
}
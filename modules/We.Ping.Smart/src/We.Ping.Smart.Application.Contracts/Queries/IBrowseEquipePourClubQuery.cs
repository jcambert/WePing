using FluentValidation;
using We.Ping.Queries;
using We.Ping.Smart.Entities.Clubs;

namespace We.Ping.Smart.Queries;
public enum TypeEquipePourClub
{
    ChampionnatFranceMasculin,
    ChampionnatFranceFeminin,
    ChampionnatFranceMixte,
    Autre
}

public static class TypeEquipePourClubExtension
{
    public static string ToCode(this TypeEquipePourClub value)
        => value switch
        {
            TypeEquipePourClub.ChampionnatFranceMasculin => "M",
            TypeEquipePourClub.ChampionnatFranceFeminin => "F",
            TypeEquipePourClub.ChampionnatFranceMixte => "A",
            _ => ""
        };
}
public interface IBrowseEquipePourClubQuery : ISmartQuery<BrowseEquipePourClubResponse>
{
    string Numero { get; set; }
    TypeEquipePourClub Type { get; set; }
}

[Dependency(ServiceLifetime.Transient, ReplaceServices = true)]
[ExposeServices(typeof(IBrowseEquipePourClubQuery))]
public sealed class BrowseEquipePourClubQuery :SmartQuery, IBrowseEquipePourClubQuery
{
    public string Numero { get; set; } = string.Empty;
    public TypeEquipePourClub Type { get; set; } = TypeEquipePourClub.ChampionnatFranceMasculin;
}

public sealed record BrowseEquipePourClubResponse(List<EquipePourClubDto> Equipes) : SmartResponse;

[Dependency(ServiceLifetime.Singleton, ReplaceServices = true)]
[ExposeServices(typeof(IValidator<BrowseEquipePourClubQuery>), typeof(IValidator<IBrowseEquipePourClubQuery>))]
public sealed class BrowseEquipePourClubQueryValidator : AbstractValidator<IBrowseEquipePourClubQuery>
{
    public BrowseEquipePourClubQueryValidator()
    {
        RuleFor(x => x).Must(x => !string.IsNullOrEmpty(x.Numero) ).WithMessage("You must specify Numero");
    }
}
using FluentValidation;
using We.Ping.Smart.Entities.Clubs;
using We.Ping.Smart.Queries;

namespace We.Ping.Queries;




public interface IBrowseClubPourDepartementQuery : ISmartQuery<BrowseClubPourDepartementResponse>
{
    string Departement { get; set; }
    string CodePostal { get; set; }
    string Ville { get; set; }
    string Numero { get; set; }
}

[Dependency(ServiceLifetime.Transient, ReplaceServices = true)]
[ExposeServices(typeof(IBrowseClubPourDepartementQuery))]
public sealed class BrowseClubPourDepartementQuery :SmartQuery, IBrowseClubPourDepartementQuery
{
    public string Departement { get; set; } = string.Empty;
    public string CodePostal { get; set; } = string.Empty;
    public string Ville { get; set; } = string.Empty;
    public string Numero { get; set; } = string.Empty;
}

public sealed record BrowseClubPourDepartementResponse(List<ClubPourDepartementDto> Clubs)
    : SmartResponse;


[Dependency(ServiceLifetime.Singleton, ReplaceServices = true)]
[ExposeServices(typeof(IValidator<BrowseClubPourDepartementQuery>), typeof(IValidator<IBrowseClubPourDepartementQuery>))]
public sealed class BrowseClubPourDepartementQueryValidator : AbstractValidator<IBrowseClubPourDepartementQuery>
{
    public BrowseClubPourDepartementQueryValidator()
    {
        RuleFor(x => x).Must(x => !string.IsNullOrEmpty(x.Numero) || !string.IsNullOrEmpty(x.Departement) || !string.IsNullOrEmpty(x.CodePostal) || !string.IsNullOrEmpty(x.Ville)).WithMessage("You must specify either Numero, Département, Ville or  Code Postal");
    }
}
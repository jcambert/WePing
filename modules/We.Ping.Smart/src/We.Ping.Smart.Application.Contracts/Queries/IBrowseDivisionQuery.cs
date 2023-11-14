using FluentValidation;
using We.Ping.Queries;
using We.Ping.Smart.Entities.Organismes;

namespace We.Ping.Smart.Queries;

public interface IBrowseDivisionQuery:ISmartQuery<BrowseDivisionResponse>
{
    string IdOrganisme { get; set; }
    string IdEpreuve {  get; set; }

    string TypeEpreuve { get; set; }
}

[Dependency(ServiceLifetime.Transient, ReplaceServices = true)]
[ExposeServices(typeof(IBrowseDivisionQuery))]
public sealed class BrowseDivisionQuery : SmartQuery, IBrowseDivisionQuery
{
    public string IdOrganisme { get; set; } = string.Empty;
    public string IdEpreuve { get; set; } = string.Empty;
    public string TypeEpreuve { get; set; } = string.Empty;
}

[Dependency(ServiceLifetime.Singleton, ReplaceServices = true)]
[ExposeServices(typeof(IValidator<BrowseDivisionQuery>), typeof(IValidator<IBrowseDivisionQuery>))]
public sealed class BrowseDivisionQueryValidator : AbstractValidator<IBrowseDivisionQuery>
{
    public BrowseDivisionQueryValidator()
    {
        RuleFor(x => x.TypeEpreuve).Must(x => x.IsEpreuve()).WithMessage(s => $"{s.TypeEpreuve} n'est pas un type d'epreuve valide");
        RuleFor(x => x.IdOrganisme).NotNull().NotEmpty().WithMessage("You must specify an Organisme Id");
        RuleFor(x => x.IdEpreuve).NotNull().NotEmpty().WithMessage("You must specify an Epreuve Id");
    }
}

public sealed record BrowseDivisionResponse(List<DivisionDto> Divisions):SmartResponse;
using FluentValidation;
using We.Ping.Queries;
using We.Ping.Smart.Joueurs;

namespace We.Ping.Smart.Queries;

public interface IBrowseJoueurPartieQuery:ISmartQuery<BrowseJoueurPartieResponse>
{
    string Licence { get; set; }
}

[Dependency(ServiceLifetime.Transient, ReplaceServices = true)]
[ExposeServices(typeof(IBrowseJoueurPartieQuery))]
public sealed class BrowseJoueurPartieQuery : SmartQuery, IBrowseJoueurPartieQuery
{
    public string Licence { get; set; } = string.Empty;
}

public sealed record BrowseJoueurPartieResponse(List<JoueurPartieDto> Parties):SmartResponse;

[Dependency(ServiceLifetime.Singleton, ReplaceServices = true)]
[ExposeServices(typeof(IValidator<BrowseJoueurPartieQuery>), typeof(IValidator<IBrowseJoueurPartieQuery>))]
public sealed class BrowseJoueurPartieQueryValidator : AbstractValidator<IBrowseJoueurPartieQuery>
{
    public BrowseJoueurPartieQueryValidator()
    {
        RuleFor(x=>x.Licence).NotNull().NotEmpty().WithMessage("Licence cannot be empty for requesting played games");
    }
}
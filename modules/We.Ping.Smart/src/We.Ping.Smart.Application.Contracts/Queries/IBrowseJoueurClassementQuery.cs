using FluentValidation;
using We.Ping.Queries;
using We.Ping.Smart.Joueurs;

namespace We.Ping.Smart.Queries;

public interface IBrowseJoueurClassementQuery : ISmartQuery<BrowseJoueurClassementResponse>
{
    string Club { get; set; }
    string Nom { get; set; }
    string Prenom { get; set; }
}

[Dependency(ServiceLifetime.Transient, ReplaceServices = true)]
[ExposeServices(typeof(IBrowseJoueurClassementQuery))]
public sealed class BrowseJoueurClassementQuery : SmartQuery, IBrowseJoueurClassementQuery
{
    public string Club { get; set; } = string.Empty;
    public string Nom { get; set; } = string.Empty;
    public string Prenom { get; set; } = string.Empty;
}

public sealed record BrowseJoueurClassementResponse(List<JoueurClassementDto> Joueurs) : SmartResponse;

[Dependency(ServiceLifetime.Singleton, ReplaceServices = true)]
[ExposeServices(typeof(IValidator<BrowseJoueurClassementQuery>), typeof(IValidator<IBrowseJoueurClassementQuery>))]
public sealed class BrowseJoueurClassementQueryValidator : AbstractValidator<IBrowseJoueurClassementQuery>
{
    public BrowseJoueurClassementQueryValidator()
    {
        RuleFor(x => x).Must(x => !string.IsNullOrEmpty(x.Club) || !string.IsNullOrEmpty(x.Nom)).WithMessage("You must specify either Club,  Nom");
        
    }
}
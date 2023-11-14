using FluentValidation;
using We.Ping.Queries;
using We.Ping.Smart.Joueurs;

namespace We.Ping.Smart.Queries;
public enum ValiditeLicence
{
    Tous = 0,
    Valide
}
public interface IBrowseJoueurLicenceSpidQuery : ISmartQuery<BrowseJoueurLicenceSpidResponse>
{
    string Licence { get; set; } 
    string Club { get; set; }
    string Nom { get; set; }
    string Prenom { get; set; }
    ValiditeLicence ValiditeLicence { get; set; }
}

[Dependency(ServiceLifetime.Transient, ReplaceServices = true)]
[ExposeServices(typeof(IBrowseJoueurLicenceSpidQuery))]
public sealed class BrowseJoueurLicenceSpidQuery : SmartQuery, IBrowseJoueurLicenceSpidQuery
{
    public string Club { get; set; } = string.Empty;
    public string Licence { get; set; } = string.Empty;
    public string Nom { get; set; } = string.Empty;
    public string Prenom { get; set; } = string.Empty;
    public ValiditeLicence ValiditeLicence { get; set; } = ValiditeLicence.Valide;
}

public sealed record BrowseJoueurLicenceSpidResponse(List<JoueurLicenceSpidDto> Joueurs) : SmartResponse;

[Dependency(ServiceLifetime.Singleton, ReplaceServices = true)]
[ExposeServices(typeof(IValidator<BrowseJoueurLicenceSpidQuery>), typeof(IValidator<IBrowseJoueurLicenceSpidQuery>))]
public sealed class BrowseJoueurLicenceSpidQueryValidator : AbstractValidator<IBrowseJoueurLicenceSpidQuery>
{
    public BrowseJoueurLicenceSpidQueryValidator()
    {
        RuleFor(x => x).Must(x => !string.IsNullOrEmpty(x.Club) || !string.IsNullOrEmpty(x.Licence) || !string.IsNullOrEmpty(x.Nom)).WithMessage("You must specify either Club, Licence or  Nom");
        
    }
}
using FluentValidation;
using We.Ping.Queries;
using We.Ping.Smart.Joueurs;

namespace We.Ping.Smart.Queries;

public interface IGetJoueurLicenceSpidQuery : ISmartQuery<GetJoueurLicenceSpidResponse>
{
    string Licence { get; set; }
}

[Dependency(ServiceLifetime.Transient, ReplaceServices = true)]
[ExposeServices(typeof(IGetJoueurLicenceSpidQuery))]
public sealed class GetJoueurLicenceSpidQuery : SmartQuery, IGetJoueurLicenceSpidQuery
{
    public string Licence { get; set; } = string.Empty;
}

public sealed record GetJoueurLicenceSpidResponse(JoueurLicenceSpidDto Joueur):SmartResponse;

[Dependency(ServiceLifetime.Singleton, ReplaceServices = true)]
[ExposeServices(typeof(IValidator<GetJoueurLicenceSpidQuery>), typeof(IValidator<IGetJoueurLicenceSpidQuery>))]
public sealed class GetJoueurLicenceSpidQueryValidator : AbstractValidator<IGetJoueurLicenceSpidQuery>
{
    public GetJoueurLicenceSpidQueryValidator()
    {
        RuleFor(x=>x.Licence).NotEmpty().WithMessage("Licence cannot be empty");
    }
}
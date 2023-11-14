using FluentValidation;
using We.Ping.Queries;
using We.Ping.Smart.Joueurs;

namespace We.Ping.Smart.Queries;

public interface IGetJoueurClassementQuery:ISmartQuery<GetJoueurClassementResponse>
{
    string Licence { get; set; }
}

[Dependency(ServiceLifetime.Transient, ReplaceServices = true)]
[ExposeServices(typeof(IGetJoueurClassementQuery))]
public sealed class GetJoueurClassementQuery : SmartQuery, IGetJoueurClassementQuery
{
    public string Licence { get; set; } = string.Empty;
}

public sealed record GetJoueurClassementResponse(JoueurClassementDto Joueur):SmartResponse;

[Dependency(ServiceLifetime.Singleton, ReplaceServices = true)]
[ExposeServices(typeof(IValidator<GetJoueurClassementQuery>), typeof(IValidator<IGetJoueurClassementQuery>))]
public sealed class GetJoueurClassementQueryValidator : AbstractValidator<IGetJoueurClassementQuery>
{
    public GetJoueurClassementQueryValidator()
    {
        RuleFor(x=>x.Licence).NotEmpty().WithMessage("Licence cannot be empty");
    }
}
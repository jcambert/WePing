using FluentValidation;
using We.Ping.Queries;
using We.Ping.Smart.Entities.Clubs;

namespace We.Ping.Smart.Queries;

public interface IGetDetailClubQuery : ISmartQuery<GetDetailClubResponse>
{
    string Numero { get; set; }
}

[Dependency(ServiceLifetime.Transient, ReplaceServices = true)]
[ExposeServices(typeof(IGetDetailClubQuery))]
public class GetDetailClubQuery :SmartQuery, IGetDetailClubQuery
{
    public string Numero { get; set; } = string.Empty;
}

public sealed record GetDetailClubResponse(List<ClubDetailDto> Clubs):SmartResponse;

[Dependency(ServiceLifetime.Singleton, ReplaceServices = true)]
[ExposeServices(typeof(IValidator<GetDetailClubQuery>), typeof(IValidator<IGetDetailClubQuery>))]
public sealed class GetDetailClubQueryValidator : AbstractValidator<IGetDetailClubQuery>
{
    public GetDetailClubQueryValidator()
    {
        RuleFor(x => x.Numero).NotNull().NotEmpty().WithMessage("Numero cannot be empty");
    }
}

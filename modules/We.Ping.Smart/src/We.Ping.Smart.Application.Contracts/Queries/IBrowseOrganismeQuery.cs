using FluentValidation;
using We.Ping.Queries;
using We.Ping.Smart.Entities.Organismes;

namespace We.Ping.Smart.Queries;

public interface IBrowseOrganismeQuery:ISmartQuery<BrowseOrganismeResponse>
{
    string Type { get; set; }   
}


[Dependency(ServiceLifetime.Transient, ReplaceServices = true)]
[ExposeServices(typeof(IBrowseOrganismeQuery))]
public class BrowseOrganismeQuery : SmartQuery, IBrowseOrganismeQuery
{
    public string Type { get; set; } = string.Empty;
}

public sealed record BrowseOrganismeResponse(List<OrganismeDto> Organismes):SmartResponse;

[Dependency(ServiceLifetime.Singleton, ReplaceServices = true)]
[ExposeServices(typeof(IValidator<BrowseOrganismeQuery>), typeof(IValidator<IBrowseOrganismeQuery>))]
public sealed class BrowseOrganismeQueryValidator : AbstractValidator<IBrowseOrganismeQuery>
{
    public BrowseOrganismeQueryValidator()
    {
        RuleFor(x => x.Type).NotNull().NotEmpty().WithMessage("Type cannot be empty");
    }
}
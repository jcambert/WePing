using FluentValidation;
using We.Ping.Queries;
using We.Ping.Smart.Entities.Organismes;

namespace We.Ping.Smart.Queries;



public sealed class TypeEpreuve {
    public static TypeEpreuve Individuelle = new("Individuelle", "I");
    public static TypeEpreuve Equipe = new("Equipe", "E");
    private TypeEpreuve(string epreuve, string value) => (Epreuve, Value) = (epreuve, value);
    public string Epreuve { get; init; }
    public string Value { get; init; }


}
public static class TypeEpreuveExtensions
{
    static TypeEpreuveExtensions()
    {
        TypeEpreuves  = new TypeEpreuve[] { TypeEpreuve.Equipe, TypeEpreuve.Individuelle };
    }
    public static TypeEpreuve[] TypeEpreuves { get; private set; } 

    public static bool IsEpreuve(this string s)=>TypeEpreuves.Where(x=>x.Value==s).Any();

    public static string GetValue(this TypeEpreuve e)=>e.Value;
    public static string GetValue(this string e) => TypeEpreuves.Where(x=>x.Epreuve==e).FirstOrDefault()?.Value ?? string.Empty;
}

public interface IBrowseEpreuveOrganismeQuery:ISmartQuery<BrowseEpreuveOrganismeResponse>
{
    string IdOrganisme { get; set; }
    string TypeEpreuve { get; set; }
}

[Dependency(ServiceLifetime.Transient, ReplaceServices = true)]
[ExposeServices(typeof(IBrowseEpreuveOrganismeQuery))]
public sealed class BrowseEpreuveOrganismeQuery : SmartQuery, IBrowseEpreuveOrganismeQuery
{
    public string IdOrganisme { get; set; } = string.Empty;
    public string TypeEpreuve { get; set; } = string.Empty;
}

public sealed record BrowseEpreuveOrganismeResponse(List<EpreuveOrganismeDto> Epreuves):SmartResponse;


[Dependency(ServiceLifetime.Singleton, ReplaceServices = true)]
[ExposeServices(typeof(IValidator<BrowseEpreuveOrganismeQuery>), typeof(IValidator<IBrowseEpreuveOrganismeQuery>))]
public sealed class BrowseEpreuveOrganismeQueryValidator : AbstractValidator<IBrowseEpreuveOrganismeQuery>
{
    public BrowseEpreuveOrganismeQueryValidator()
    {
        RuleFor(x => x.TypeEpreuve).Must(x=>x.IsEpreuve()).WithMessage(s => $"{s.TypeEpreuve} n'est pas un type d'epreuve valide");
        RuleFor(x => x.IdOrganisme).NotNull().NotEmpty().WithMessage("You must specify an Organisme Id");
    }
}
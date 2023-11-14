using System.Xml.Serialization;
using Volo.Abp.Application.Services;
using We.Mediatr;
using We.Ping.Smart.Localization;
using We.Ping.Smart.Queries;

namespace We.Ping.Smart;

public abstract class AbstractSmartAppService : ApplicationService
{
    protected IMediator Mediator => LazyServiceProvider.LazyGetRequiredService<IMediator>();
    protected AbstractSmartAppService()
    {
        LocalizationResource = typeof(SmartResource);
        ObjectMapperContext = typeof(SmartApplicationModule);
    }
}

public class SmartAppService:AbstractSmartAppService, ISmartAppService
{
    public Task<Result<BrowseClubPourDepartementResponse>> BrowseClubPourDepartement(IBrowseClubPourDepartementQuery query)
    => Mediator.Send(query).AsTaskWrap();

    public Task<Result<GetDetailClubResponse>> GetClubDetail(IGetDetailClubQuery query)
    => Mediator.Send(query).AsTaskWrap();
    public Task<Result<BrowseEquipePourClubResponse>> BrowseEquipePourClub(IBrowseEquipePourClubQuery query)
    => Mediator.Send(query).AsTaskWrap();

    public Task<Result<NombreEquipePourClubResponse>> NombreEquipePourClub(NombreEquipePourClubQuery query)
    => Mediator.Send(query).AsTaskWrap();

    public Task<Result<BrowseOrganismeResponse>> BrowseOrganismes(IBrowseOrganismeQuery query)
    => Mediator.Send(query).AsTaskWrap();

    public Task<Result<BrowseJoueurClassementResponse>> BrowseJoueurClassement(IBrowseJoueurClassementQuery query)
     => Mediator.Send(query).AsTaskWrap();

    public Task<Result<BrowseJoueurLicenceSpidResponse>> BrowseJoueurLicenceSpid(IBrowseJoueurLicenceSpidQuery query)
    => Mediator.Send(query).AsTaskWrap();

    public Task<Result<GetJoueurClassementResponse>> GetJoueurClassement(IGetJoueurClassementQuery query)
    => Mediator.Send(query).AsTaskWrap();

    public Task<Result<GetJoueurLicenceSpidResponse>> GetJoueurLicenceSpid(IGetJoueurLicenceSpidQuery query)
    => Mediator.Send(query).AsTaskWrap();

    public Task<Result<BrowseJoueurPartieResponse>> BrowseJoueurParties(IBrowseJoueurPartieQuery query)
    => Mediator.Send(query).AsTaskWrap();

    public Task<Result<BrowseEpreuveOrganismeResponse>> BrowseEpreuveOrganisme(IBrowseEpreuveOrganismeQuery query)
    => Mediator.Send(query).AsTaskWrap();

    public Task<Result<BrowseDivisionResponse>> BrowseDivisions(IBrowseDivisionQuery query)
    => Mediator.Send(query).AsTaskWrap();
}
public interface IDeserializeService<T>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    Result<T> To(Stream data);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="reader"></param>
    /// <returns></returns>
    Result<T> To(TextReader reader);
}

[Dependency(ServiceLifetime.Transient, ReplaceServices = true)]
[ExposeServices(typeof(IDeserializeService<>))]
public class DeserializeService<T> : IDeserializeService<T>
{
    public XmlSerializer Serializer { get; init; } = new XmlSerializer(typeof(T));
    //public DeserializeService() => (Serializer) = (new XmlSerializer(typeof(T)));
    ///<inheritdoc/>
    public Result<T> To(Stream data) =>ToResult(Serializer.Deserialize(data));
    ///<inheritdoc/>
    public Result<T> To(TextReader reader) => ToResult(Serializer.Deserialize(reader));
    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    private Result<T> ToResult(object? value)
    {
        if (value == null)
            return Result.Failure<T>($"Nothing to deserialize {typeof(T)}");
        var result = (T)value;
        if (result == null)
            return Result.Failure<T>($"Cannot deserialize to type {typeof(T)}");
        return Result.Success(result);

    }
    
}
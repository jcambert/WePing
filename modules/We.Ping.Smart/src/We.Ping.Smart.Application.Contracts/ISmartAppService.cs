using We.Ping.Queries;
using We.Ping.Smart.Queries;
using We.Results;

namespace We.Ping.Smart;

public interface ISmartAppService
{
    Task<Result<BrowseClubPourDepartementResponse>> BrowseClubPourDepartement(IBrowseClubPourDepartementQuery query);
    Task<Result<GetDetailClubResponse>> GetClubDetail(IGetDetailClubQuery query);

    Task<Result<BrowseEquipePourClubResponse>> BrowseEquipePourClub(IBrowseEquipePourClubQuery query);
    Task<Result<NombreEquipePourClubResponse>> NombreEquipePourClub(NombreEquipePourClubQuery query);
    Task<Result<BrowseOrganismeResponse>> BrowseOrganismes(IBrowseOrganismeQuery query);
    Task<Result<BrowseJoueurClassementResponse>> BrowseJoueurClassement(IBrowseJoueurClassementQuery query);
    Task<Result<BrowseJoueurLicenceSpidResponse>> BrowseJoueurLicenceSpid(IBrowseJoueurLicenceSpidQuery query);
    Task<Result<GetJoueurClassementResponse>> GetJoueurClassement(IGetJoueurClassementQuery q);
    Task<Result<GetJoueurLicenceSpidResponse>> GetJoueurLicenceSpid(IGetJoueurLicenceSpidQuery query);
    Task<Result<BrowseJoueurPartieResponse>> BrowseJoueurParties(IBrowseJoueurPartieQuery q);
    Task<Result<BrowseEpreuveOrganismeResponse>> BrowseEpreuveOrganisme(IBrowseEpreuveOrganismeQuery q);
    Task<Result<BrowseDivisionResponse>> BrowseDivisions(IBrowseDivisionQuery q);
}

using Volo.Abp.Application.Dtos;

namespace We.Ping.Smart.Entities.Clubs;

public class ClubDetailDto:EntityDto<string>
{
    public string Numero { get; set; } = string.Empty;

    public string Salle { get; set; } = string.Empty;

    public string AdresseSalle1 { get; set; } = string.Empty;

    public string AdresseSalle2 { get; set; } = string.Empty;

    public string AdresseSalle3 { get; set; } = string.Empty;

    public string CodePostalSalle { get; set; } = string.Empty;

    public string VilleSalle { get; set; } = string.Empty;

    public string SiteInternet { get; set; } = string.Empty;

    public string NomCorrespondant { get; set; } = string.Empty;

    public string PrenomCorrespondant { get; set; } = string.Empty;

    public string MailCorrespondant { get; set; } = string.Empty;

    public string TelephoneCorrespondant { get; set; } = string.Empty;

    public string Latitude { get; set; } = string.Empty;

    public string Longitude { get; set; } = string.Empty;
}

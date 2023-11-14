using System.Xml.Serialization;
using Volo.Abp.Caching;
using Volo.Abp.Domain.Entities;

namespace We.Ping.Smart.Entities;

[XmlType(AnonymousType = true)]
[XmlRoot(ElementName = "liste")]
public sealed class ListClubDetail
{
    [XmlElement(ElementName = "club")]
    public List<ClubDetail> Liste { get; set; } = new List<ClubDetail>() { };
}

[CacheName(nameof(ClubDetail))]
public class ClubDetail : Entity
{
    [XmlElement(ElementName = "idclub")]
    public string Id { get; set; } = string.Empty;

    [XmlElement(ElementName = "numero")]
    public string Numero { get; set; } = string.Empty;

    [XmlElement(ElementName = "nomsalle")]
    public string Salle { get; set; } = string.Empty;

    [XmlElement(ElementName = "adressesalle1")]
    public string AdresseSalle1 { get; set; } = string.Empty;

    [XmlElement(ElementName = "adressesalle2")]
    public string AdresseSalle2 { get; set; } = string.Empty;

    [XmlElement(ElementName = "adressesalle3")]
    public string AdresseSalle3 { get; set; } = string.Empty;

    [XmlElement(ElementName = "codepsalle")]
    public string CodePostalSalle { get; set; } = string.Empty;

    [XmlElement(ElementName = "villesalle")]
    public string VilleSalle { get; set; } = string.Empty;

    [XmlElement(ElementName = "web")]
    public string SiteInternet { get; set; } = string.Empty;

    [XmlElement(ElementName = "nomcor")]
    public string NomCorrespondant { get; set; } = string.Empty;

    [XmlElement(ElementName = "prenomcor")]
    public string PrenomCorrespondant { get; set; } = string.Empty;

    [XmlElement(ElementName = "mailcor")]
    public string MailCorrespondant { get; set; } = string.Empty;

    [XmlElement(ElementName = "telcor")]
    public string TelephoneCorrespondant { get; set; } = string.Empty;

    [XmlElement(ElementName = "latitude")]
    public string Latitude { get; set; } = string.Empty;

    [XmlElement(ElementName = "longitude")]
    public string Longitude { get; set; } = string.Empty;

    public override object[] GetKeys() => new object[] { Id };
}

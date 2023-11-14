using System.Xml.Serialization;
using Volo.Abp.Caching;
using Volo.Abp.Domain.Entities;

namespace We.Ping.Smart.Entities;


[XmlType(AnonymousType = true)]
[XmlRoot(ElementName = "liste")]
public sealed class ListClubPourDepartement
{
    [XmlElement(ElementName = "club")]
    public List<ClubPourDepartement> Liste { get; set; } = new List<ClubPourDepartement>() { };
}

[CacheName(nameof(ClubPourDepartement))]
public sealed class ClubPourDepartement : Entity
{
    [XmlElement(ElementName = "idclub")]
    public string Id { get; set; } = string.Empty;

    [XmlElement(ElementName = "numero")]
    public string Numero { get; set; } = string.Empty;

    [XmlElement(ElementName = "nom")]
    public string Nom { get; set; } = string.Empty;

    [XmlElement(ElementName = "validation")]
    public string Validation { get; set; } = string.Empty;

    [XmlElement(ElementName = "typeclub")]
    public string TypeClub { get; set; } = string.Empty;

    public override object[] GetKeys() => new object[] { Id };
}

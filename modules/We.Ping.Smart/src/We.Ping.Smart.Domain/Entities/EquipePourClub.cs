using System.Xml.Serialization;
using Volo.Abp.Caching;
using Volo.Abp.Domain.Entities;

namespace We.Ping.Smart.Entities;

[XmlType(AnonymousType = true)]
[XmlRoot(ElementName = "liste")]
public sealed class ListEquipePourClub
{
    [XmlElement(ElementName = "equipe")]
    public List<EquipePourClub> Liste { get; set; } = new List<EquipePourClub>() { };
}

[CacheName(nameof(EquipePourClub))]
public class EquipePourClub : Entity
{
    [XmlElement(ElementName = "libequipe")]
    public string Equipe { get; set; } = string.Empty;

    [XmlElement(ElementName = "libdivision")]
    public string Division { get; set; } = string.Empty;

    [XmlElement(ElementName = "idepr")]
    public string IdEpreuve { get; set; } = string.Empty;

    [XmlElement(ElementName = "libepr")]
    public string Epreuve { get; set; } = string.Empty;

    [XmlElement(ElementName = "liendivision")]
    public string LienEpreuve { get; set; } = string.Empty;

    public override object[] GetKeys() => new object[] { IdEpreuve, Equipe };
}

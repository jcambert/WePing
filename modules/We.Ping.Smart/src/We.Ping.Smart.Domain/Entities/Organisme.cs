using System.Xml.Serialization;
using Volo.Abp.Caching;
using Volo.Abp.Domain.Entities;

namespace We.Ping.Smart.Entities;

[XmlType(AnonymousType = true)]
[XmlRoot(ElementName = "liste")]
public sealed class ListOrganisme
{
    [XmlElement(ElementName = "organisme")]
    public List<Organisme> Liste { get; set; } = new List<Organisme>() { };
}

[CacheName(nameof(Organisme))]
public class Organisme:Entity
{
    [XmlElement(ElementName = "idclub")]
    public string Id { get; set; }=string.Empty;

    [XmlElement(ElementName = "libelle")]
    public string Libelle { get; set; } = string.Empty;

    [XmlElement(ElementName = "code")]
    public string Code { get; set; } = string.Empty;

    [XmlElement(ElementName = "idPere")]
    public string IdParent { get; set; } = string.Empty;

    public override object[] GetKeys() => new object[] { Id };
}

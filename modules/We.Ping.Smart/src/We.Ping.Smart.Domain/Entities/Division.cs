using System.Xml.Serialization;
using Volo.Abp.Domain.Entities;

namespace We.Ping.Smart.Entities;

[XmlRoot(ElementName = "liste")]
public sealed class ListDivision
{
    [XmlElement(ElementName = "division")]
    public List<Division> Divisions { get; set; } = new List<Division>() { };
}
public class Division : Entity
{
    [XmlElement(ElementName = "iddivision")]
    public string Id { get; set; } = string.Empty;
    [XmlElement(ElementName = "libelle")]
    public string Libelle { get; set; } = string.Empty;
    public override object?[] GetKeys()
    => new object[] { Id };
}

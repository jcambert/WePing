using System.Xml.Serialization;
using Volo.Abp.Caching;
using Volo.Abp.Domain.Entities;

namespace We.Ping.Smart.Entities;


[XmlRoot(ElementName = "liste")]
public class ListeEpreuveOrganisme
{
    [XmlElement(ElementName = "epreuve")] 
    public List<EpreuveOrganisme> Epreuves { get; set; }   = new List<EpreuveOrganisme>();
}

[CacheName(nameof(EpreuveOrganisme))]
public class EpreuveOrganisme : Entity
{
    [XmlElement(ElementName = "idepreuve")]
    public string Id { get; set; } = string.Empty;

    [XmlElement(ElementName = "idorga")]
    public string Organisme { get; set; } = string.Empty;

    [XmlElement(ElementName = "libelle")]
    public string  Libelle { get; set; } = string.Empty;

    [XmlElement(ElementName = "typepreuve")]
    public string Type { get; set; } = string.Empty;

    public override object?[] GetKeys() => new object[] { Id };
}

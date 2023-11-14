using System.Xml.Serialization;
using Volo.Abp.Caching;
using Volo.Abp.Domain.Entities;

namespace We.Ping.Smart.Entities;


[XmlRoot(ElementName = "liste")]
public class ListeJoueurPartie
{
    [XmlElement(ElementName = "partie")]
    public List<JoueurPartie> Resultats { get; set; } = new List<JoueurPartie>() { };
}

[CacheName(nameof(JoueurPartie))]
public class JoueurPartie : Entity
{

    [XmlElement(ElementName = "date")]
    public string Date { get; set; } = string.Empty;

    [XmlElement(ElementName = "nom")]
    public string NomAdversaire { get; set; } = string.Empty;

    [XmlElement(ElementName = "classement")]
    public string ClassementAdversaire { get; set; } = string.Empty;

    [XmlElement(ElementName = "epreuve")]
    public string Epreuve { get; set; } = string.Empty;

    [XmlElement(ElementName = "victoire")]
    public string Victoire { get; set; } = string.Empty;

    [XmlElement(ElementName = "forfait")]
    public string Forfait { get; set; } = string.Empty;

    [XmlElement(ElementName = "coefchamp")]
    public string Coeficient { get; set; } = string.Empty;

    [XmlElement(ElementName = "idpartie")]
    public string Id { get; set; } = string.Empty;

    public override object?[] GetKeys()
    => new object[] { Id };
}

using System.Xml.Serialization;
using Volo.Abp.Caching;
using Volo.Abp.Domain.Entities;

namespace We.Ping.Smart.Entities;
[XmlType(AnonymousType = true)]
[XmlRoot(ElementName = "liste")]
public sealed class ListJoueurClassement
{
    [XmlElement(ElementName = "joueur")]
    public List<JoueurClassement> Liste { get; set; } = new List<JoueurClassement>() { };
}

[CacheName(nameof(JoueurClassement))]
public class JoueurClassement:Entity
{
    [XmlElement(ElementName = "licence")]
    public string Licence { get; set; } = string.Empty;

    [XmlElement(ElementName = "nom")]
    public string Nom { get; set; } = string.Empty;

    [XmlElement(ElementName = "prenom")]
    public string Prenom { get; set; } = string.Empty;

    [XmlElement(ElementName = "club")]
    public string NumeroClub { get; set; }=string.Empty;

    [XmlElement(ElementName = "nclub")]
    public string Club { get; set; } = string.Empty;

    [XmlElement(ElementName = "clast")]
    public string Classement { get; set; } = string.Empty;


    [XmlElement(ElementName = "natio")]
    public string Nationalite { get; set; } = string.Empty;

    [XmlElement(ElementName = "clglob")]
    public string ClassementGlobal { get; set; } = string.Empty;

    [XmlElement(ElementName = "point")]
    public string PointsMensuel { get; set; } = string.Empty;

    [XmlElement(ElementName = "aclglob")]
    public string AncienClassementGlobal { get; set; } = string.Empty;

    [XmlElement(ElementName = "apoint")]
    public string AncienPoints { get; set; } = string.Empty;

    [XmlElement(ElementName = "categ")]
    public string CategorieAge { get; set; } = string.Empty;

    [XmlElement(ElementName = "rangreg")]
    public string RangRegional { get; set; } = string.Empty;

    [XmlElement(ElementName = "rangdep")]
    public string RangDepartmental { get; set; } = string.Empty;

    [XmlElement(ElementName = "valcla")]
    public string PointsOfficiel { get; set; } = string.Empty;

    [XmlElement(ElementName = "clpro")]
    public string PropositionClassement { get; set; } = string.Empty;

    [XmlElement(ElementName = "valinit")]
    public string PointDebutSaison { get; set; } = string.Empty;



    public override object?[] GetKeys()
    =>new object?[] { Licence };
}

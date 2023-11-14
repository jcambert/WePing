using System.Xml.Serialization;
using Volo.Abp.Caching;
using Volo.Abp.Domain.Entities;

namespace We.Ping.Smart.Entities;
[XmlType(AnonymousType = true)]
[XmlRoot(ElementName = "liste")]
public sealed class ListJoueurLicenceSpid
{
    [XmlElement(ElementName = "joueur")]
    public List<JoueurLicenceSpid> ListeJoueurs { get; set; } = new List<JoueurLicenceSpid>() { };

    [XmlElement(ElementName = "licence")]
    public List<JoueurLicenceSpid_> ListeLicences { get; set; } = new List<JoueurLicenceSpid_>() { };

}


[CacheName(nameof(JoueurLicenceSpid))]
public class JoueurLicenceSpid : Entity
{
    

    [XmlElement(ElementName = "licence")]
    public string Licence { get; set; } = string.Empty;

    [XmlElement(ElementName = "nom")]
    public string Nom { get; set; } = string.Empty;

    [XmlElement(ElementName = "prenom")]
    public string Prenom { get; set; } = string.Empty;

    [XmlElement(ElementName = "club")]
    public string NumeroClub { get; set; } = string.Empty;

    [XmlElement(ElementName = "nclub")]
    public string Club { get; set; } = string.Empty;

    [XmlElement(ElementName = "sexe")]
    public string Sexe { get; set; } = string.Empty;

    [XmlElement(ElementName = "type")]
    public string Type { get; set; } = string.Empty;

    [XmlElement(ElementName = "certif")]
    public string CertificatMedical { get; set; } = string.Empty;

    [XmlElement(ElementName = "validation")]
    public string Validation { get; set; } = string.Empty;

    [XmlElement(ElementName = "echelon")]
    public string Echelon { get; set; } = string.Empty;

    [XmlElement(ElementName = "place")]
    public string Place { get; set; } = string.Empty;

    [XmlElement(ElementName = "points")]
    public int Points { get; set; }

    [XmlElement(ElementName = "cat")]
    public string CategorieAge { get; set; } = string.Empty;

    [XmlElement(ElementName = "pointm")]
    public int PointsMensuels { get; set; }

    [XmlElement(ElementName = "apointm")]
    public int AncienPointsMensuels { get; set; }

    [XmlElement(ElementName = "initm")]
    public int PointsDebutSaison { get; set; }

    public override object?[] GetKeys()
    => new object?[] { Licence };
}


[CacheName(nameof(JoueurLicenceSpid_))]
public class JoueurLicenceSpid_ : Entity
{
    [XmlElement(ElementName = "idlicence")]
    public string IdLicence { get; set; } = string.Empty; 

    [XmlElement(ElementName = "licence")]
    public string Licence { get; set; } = string.Empty;

    [XmlElement(ElementName = "nom")]
    public string Nom { get; set; } = string.Empty;

    [XmlElement(ElementName = "prenom")]
    public string Prenom { get; set; } = string.Empty;

    [XmlElement(ElementName = "numclub")]
    public string NumeroClub { get; set; }=string.Empty;

    [ XmlElement(ElementName = "nomclub")]
    public string Club { get; set; } = string.Empty;

    [XmlElement(ElementName = "sexe")]
    public string Sexe { get; set; } = string.Empty;

    [XmlElement(ElementName = "type")]
    public string Type { get; set; } = string.Empty;

    [XmlElement(ElementName = "certif")]
    public string CertificatMedical { get; set; } = string.Empty;

    [XmlElement(ElementName = "validation")]
    public string Validation { get; set; } = string.Empty;

    [XmlElement(ElementName = "echelon")]
    public string Echelon { get; set; } = string.Empty;

    [XmlElement(ElementName = "place")]
    public string Place { get; set; } = string.Empty;

    [XmlElement(ElementName = "point")]
    public int Points { get; set; }

    [XmlElement(ElementName = "cat")]
    public string CategorieAge { get; set; } = string.Empty;

    [XmlElement(ElementName = "pointm")]
    public string PointsMensuels { get; set; } = string.Empty;

    [XmlElement(ElementName = "apointm")]
    public string AncienPointsMensuels { get; set; } = string.Empty;

    [XmlElement(ElementName = "initm")]
    public string PointsDebutSaison { get; set; } = string.Empty;

    public override object?[] GetKeys()
    =>new object?[] { Licence };
}

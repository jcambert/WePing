using Volo.Abp.Application.Dtos;

namespace We.Ping.Smart.Joueurs;

public class JoueurLicenceSpidDto : EntityDto
{
    public string IdLicence { get; set; } = string.Empty;

    public string Licence { get; set; } = string.Empty;

    public string Nom { get; set; } = string.Empty;

    public string Prenom { get; set; } = string.Empty;

    public string NumeroClub { get; set; } = string.Empty;

    public string Club { get; set; } = string.Empty;

    public string Sexe { get; set; } = string.Empty;

    public string Type { get; set; } = string.Empty;

    public string CertificatMedical { get; set; } = string.Empty;

    public string Validation { get; set; } = string.Empty;

    public string Echelon { get; set; } = string.Empty;

    public string Place { get; set; } = string.Empty;

    public int Points { get; set; }

    public string CategorieAge { get; set; } = string.Empty;

    public string PointsMensuels { get; set; } = string.Empty;

    public string AncienPointsMensuels { get; set; } = string.Empty;

    public string PointsDebutSaison { get; set; } = string.Empty;
}

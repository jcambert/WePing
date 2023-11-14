using Volo.Abp.Application.Dtos;

namespace We.Ping.Smart.Joueurs;

public class JoueurClassementDto:EntityDto
{
    public string Licence { get; set; } = string.Empty;

    public string Nom { get; set; } = string.Empty;

    public string Prenom { get; set; } = string.Empty;

    public string NumeroClub { get; set; } = string.Empty;

    public string Club { get; set; } = string.Empty;
    public string Classement { get; set; } = string.Empty;

    public string Nationalite { get; set; } = string.Empty;

    public string ClassementGlobal { get; set; } = string.Empty;

    public string PointsMensuel { get; set; } = string.Empty;

    public string AncienClassementGlobal { get; set; } = string.Empty;

    public string AncienPoints { get; set; } = string.Empty;

    public string CategorieAge { get; set; } = string.Empty;

    public string RangRegional { get; set; } = string.Empty;

    public string RangDepartmental { get; set; } = string.Empty;

    public string PointsOfficiel { get; set; } = string.Empty;

    public string PropositionClassement { get; set; } = string.Empty;


    public string PointDebutSaison { get; set; } = string.Empty;
}

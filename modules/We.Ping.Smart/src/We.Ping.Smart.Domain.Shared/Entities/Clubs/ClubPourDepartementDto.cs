using Volo.Abp.Application.Dtos;

namespace We.Ping.Smart.Entities.Clubs;

[Serializable]

public sealed class ClubPourDepartementDto : EntityDto<string>
{
    //public string Id { get; set; } = string.Empty;
    public string Numero { get; set; } = string.Empty;
    public string Nom { get; set; } = string.Empty;
    public string Validation { get; set; } = string.Empty;
    public string TypeClub { get; set; } = string.Empty;
    public ClubDetailDto? Details { get; set; }
    public bool IsLoadingDetail { get; set; }
    public int NombreEquipe { get; set; }
    public List<EquipePourClubDto> Equipes { get; set; } = new();

}

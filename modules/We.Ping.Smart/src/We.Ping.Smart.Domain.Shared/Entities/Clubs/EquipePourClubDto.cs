using Volo.Abp.Application.Dtos;

namespace We.Ping.Smart.Entities.Clubs;

public class EquipePourClubDto:EntityDto
{
    public string Equipe { get; set; } = string.Empty;
    public string Division { get; set; } = string.Empty;
    public string IdEpreuve { get; set; } = string.Empty;
    public string Epreuve { get; set; } = string.Empty;
    public string LienEpreuve { get; set; } = string.Empty;
    public string Phase { get; set; }=string.Empty;
    public string CodeOrganisme { get; set; } = string.Empty;
    public string CodeEpreuve { get; set; } = string.Empty;
    public object Poule { get; set; }
}

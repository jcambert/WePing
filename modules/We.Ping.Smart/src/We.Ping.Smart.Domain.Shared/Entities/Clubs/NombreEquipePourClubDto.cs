using Volo.Abp.Application.Dtos;

namespace We.Ping.Smart.Entities.Clubs;

public class NombreEquipePourClubDto : EntityDto {
    public string Numero { get; set; } = string.Empty;
    public int Nombre { get; set; }
}

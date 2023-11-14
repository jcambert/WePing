using Volo.Abp.Application.Dtos;

namespace We.Ping.Smart.Entities.Organismes;

public class OrganismeDto : EntityDto<string>
{

    public string Libelle { get; set; } = string.Empty;

    public string Code { get; set; } = string.Empty;

    public string IdParent { get; set; } = string.Empty;
}

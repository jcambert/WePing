using Volo.Abp.Application.Dtos;

namespace We.Ping.Smart.Entities.Organismes;

public class EpreuveOrganismeDto:EntityDto
{
    public string Id { get; set; } = string.Empty;

    public string Organisme { get; set; } = string.Empty;

    public string Libelle { get; set; } = string.Empty;

    public string Type { get; set; } = string.Empty;
}

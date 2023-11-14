using Volo.Abp.Application.Dtos;

namespace We.Ping.Smart.Entities.Organismes;

public class DivisionDto:EntityDto
{
    public string Id { get; set; } = string.Empty;
    public string Libelle { get; set; } = string.Empty;
}

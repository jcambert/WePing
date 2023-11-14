using Volo.Abp.Caching;
using Volo.Abp.Domain.Entities;

namespace We.Ping.Smart.Entities;

[CacheName(nameof(NombreEquipePourClub))]
public class NombreEquipePourClub : Entity {

    public string Numero { get; set; } = string.Empty;
    public int Nombre { get; set; }

    public override object?[] GetKeys()
    => new object[] { Numero };
} 

using System.Threading.Tasks;

namespace We.Ping.Data;

public interface IPingDbSchemaMigrator
{
    Task MigrateAsync();
}

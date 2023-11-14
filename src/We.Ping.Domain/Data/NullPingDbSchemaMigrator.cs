using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace We.Ping.Data;

/* This is used if database provider does't define
 * IPingDbSchemaMigrator implementation.
 */
public class NullPingDbSchemaMigrator : IPingDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}

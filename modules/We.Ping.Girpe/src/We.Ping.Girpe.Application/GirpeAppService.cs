using We.Ping.Girpe.Localization;
using Volo.Abp.Application.Services;

namespace We.Ping.Girpe;

public abstract class GirpeAppService : ApplicationService
{
    protected GirpeAppService()
    {
        LocalizationResource = typeof(GirpeResource);
        ObjectMapperContext = typeof(GirpeApplicationModule);
    }
}

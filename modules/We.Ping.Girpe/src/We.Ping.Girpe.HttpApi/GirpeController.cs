using We.Ping.Girpe.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace We.Ping.Girpe;

public abstract class GirpeController : AbpControllerBase
{
    protected GirpeController()
    {
        LocalizationResource = typeof(GirpeResource);
    }
}

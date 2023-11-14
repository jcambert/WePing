using We.Ping.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace We.Ping.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class PingController : AbpControllerBase
{
    protected PingController()
    {
        LocalizationResource = typeof(PingResource);
    }
}

using We.Ping.Smart.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace We.Ping.Smart;

public abstract class SmartController : AbpControllerBase
{
    protected SmartController()
    {
        LocalizationResource = typeof(SmartResource);
    }
}

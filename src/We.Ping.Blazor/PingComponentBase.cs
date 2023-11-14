using We.Ping.Localization;
using Volo.Abp.AspNetCore.Components;

namespace We.Ping.Blazor;

public abstract class PingComponentBase : AbpComponentBase
{
    protected PingComponentBase()
    {
        LocalizationResource = typeof(PingResource);
    }
}

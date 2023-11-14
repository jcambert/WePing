using System;
using System.Collections.Generic;
using System.Text;
using We.Ping.Localization;
using Volo.Abp.Application.Services;

namespace We.Ping;

/* Inherit your application services from this class.
 */
public abstract class PingAppService : ApplicationService
{
    protected PingAppService()
    {
        LocalizationResource = typeof(PingResource);
    }
}

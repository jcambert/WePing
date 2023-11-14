using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace We.Ping.Blazor;

[Dependency(ReplaceServices = true)]
public class PingBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "Ping";
}

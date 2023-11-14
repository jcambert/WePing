using Volo.Abp.Settings;

namespace We.Ping.Settings;

public class PingSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(PingSettings.MySetting1));
    }
}

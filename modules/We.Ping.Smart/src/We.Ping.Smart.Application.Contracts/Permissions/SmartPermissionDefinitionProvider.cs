using We.Ping.Smart.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace We.Ping.Smart.Permissions;

public class SmartPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(SmartPermissions.GroupName, L("Permission:Smart"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<SmartResource>(name);
    }
}

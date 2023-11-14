using We.Ping.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace We.Ping.Permissions;

public class PingPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(PingPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(PingPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<PingResource>(name);
    }
}

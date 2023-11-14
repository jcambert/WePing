using Volo.Abp.Reflection;

namespace We.Ping.Smart.Permissions;

public class SmartPermissions
{
    public const string GroupName = "Smart";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(SmartPermissions));
    }
}

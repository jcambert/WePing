using Volo.Abp.Reflection;

namespace We.Ping.Girpe.Permissions;

public class GirpePermissions
{
    public const string GroupName = "Girpe";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(GirpePermissions));
    }
}

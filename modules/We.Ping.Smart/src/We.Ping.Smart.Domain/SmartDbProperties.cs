namespace We.Ping.Smart;

public static class SmartDbProperties
{
    public static string DbTablePrefix { get; set; } = "Smart";

    public static string? DbSchema { get; set; } = null;

    public const string ConnectionStringName = "Smart";
}

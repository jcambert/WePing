namespace We.Ping.Girpe;

public static class GirpeDbProperties
{
    public static string DbTablePrefix { get; set; } = "Girpe";

    public static string? DbSchema { get; set; } = null;

    public const string ConnectionStringName = "Girpe";
}

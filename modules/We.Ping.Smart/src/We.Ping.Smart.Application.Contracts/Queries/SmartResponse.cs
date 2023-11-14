namespace We.Ping.Queries;


public enum LoadFrom
{
    Cache,
    Database,
    Spid
}

public record SmartResponse() : WeM.Response
{
    public LoadFrom LoadFrom { get;  set; } = LoadFrom.Cache;
    public string Uri { get;  set; } = string.Empty;
}


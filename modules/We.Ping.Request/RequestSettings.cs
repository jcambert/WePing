namespace We.Ping.Request;

/// <summary>
///
/// </summary>
public sealed class RequestSettings
{
    /// <summary>
    ///
    /// </summary>
    public static string DEFAULT_BASE_URL { get; } = "http://www.fftt.com/mobile/pxml/";
    public static string DEFAULT_EXTENSION { get; } = "php";

    /// <summary>
    ///
    /// </summary>
    public string BaseUrl { get; set; } = DEFAULT_BASE_URL;

    public string Extension { get; set; } = DEFAULT_EXTENSION;
}

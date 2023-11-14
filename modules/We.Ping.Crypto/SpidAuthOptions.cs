namespace We.Ping.Crypto;

/// <summary>
///
/// </summary>
/// <param name="tm">TimeStamp</param>
/// <param name="tmc">TimeStamp Encrypted</param>
public sealed record SpidAuth(string tm, string tmc);

/// <summary>
///
/// </summary>
public sealed class SpidAuthOptions
{
    /// <summary>
    ///
    /// </summary>
    public string Password { get; set; } = string.Empty;

    /// <summary>
    ///
    /// </summary>
    public string AppId { get; set; } = string.Empty;

    /// <summary>
    ///
    /// </summary>
    public string Serie { get; set; } = string.Empty;
}

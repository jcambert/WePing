namespace We.Ping.Crypto;

/// <summary>
///
/// </summary>
public interface ITokenGeneratorQuery : WeM.IQuery<TokenGeneratorResponse> { }

/// <inheritdoc/>
public sealed class TokenGeneratorQuery : ITokenGeneratorQuery { }

/// <summary>
///
/// </summary>
/// <param name="Authentication"></param>
public sealed record TokenGeneratorResponse(SpidAuth Authentication) : WeM.Response;

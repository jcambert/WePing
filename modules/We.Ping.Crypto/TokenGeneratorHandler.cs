using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.DependencyInjection;
using We.AbpExtensions;
using We.Results;

namespace We.Ping.Crypto;

/// <summary>
///
/// </summary>
public sealed class TokenGeneratorHandler
    : AbpHandler.With<TokenGeneratorQuery, TokenGeneratorResponse>
{
    private readonly ITokenGeneratorService tokenService;

    /// <inheritdoc/>
    public TokenGeneratorHandler(IAbpLazyServiceProvider serviceProvider) : base(serviceProvider)
    {
        this.tokenService = LazyServiceProvider.GetRequiredService<ITokenGeneratorService>();
    }

    /// <inheritdoc/>
    protected override async Task<Result<TokenGeneratorResponse>> InternalHandle(
        TokenGeneratorQuery request,
        CancellationToken cancellationToken
    )
    {
        await base.InternalHandle(request, cancellationToken);
        var auth = tokenService.Create();
        return new TokenGeneratorResponse(auth);
    }
}

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text;
using Volo.Abp.DependencyInjection;

namespace We.Ping.Crypto;

internal interface ITokenGeneratorService
{
    string Key { get; }
    SpidAuth Create();
}
[Dependency(ServiceLifetime.Transient, ReplaceServices = true)]
[ExposeServices(typeof(ITokenGeneratorService))]
internal class TokenGeneratorService : ITokenGeneratorService
{
    private readonly SpidAuthOptions options;

    public string Key { get; private set; }

    public TokenGeneratorService(IOptions<SpidAuthOptions> opt)
    {
        this.options = opt.Value;
        this.Key = CreateKey(this.options.Password);
    }

    private string CreateKey(string password)
    {
        var kkey = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(password));
        return BitConverter.ToString(kkey).Replace("-", string.Empty).ToLower();
    }

    public SpidAuth Create()
    {
        var tm = DateTime.Now.ToString("yyyyMMddhhmmssfff");
        return new SpidAuth(tm, GetHash(tm, Key));
    }

    private static String GetHash(String text, String key)
    {
        UTF8Encoding encoding = new UTF8Encoding();

        Byte[] textBytes = encoding.GetBytes(text);
        Byte[] keyBytes = encoding.GetBytes(key);

        Byte[] hashBytes;

        using (HMACSHA1 hash = new HMACSHA1(keyBytes))
            hashBytes = hash.ComputeHash(textBytes);

        return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
    }
}

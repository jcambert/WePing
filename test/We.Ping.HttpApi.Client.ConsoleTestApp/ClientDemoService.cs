using System;
using System.Threading.Tasks;
using Volo.Abp.Account;
using Volo.Abp.DependencyInjection;

namespace We.Ping.HttpApi.Client.ConsoleTestApp;

/// <summary>
/// 
/// </summary>
public class ClientDemoService : ITransientDependency
{
    private readonly IProfileAppService _profileAppService;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="profileAppService"></param>
    public ClientDemoService(IProfileAppService profileAppService)
    {
        _profileAppService = profileAppService;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public async Task RunAsync()
    {
        var output = await _profileAppService.GetAsync();
        Console.WriteLine($"UserName : {output.UserName}");
        Console.WriteLine($"Email    : {output.Email}");
        Console.WriteLine($"Name     : {output.Name}");
        Console.WriteLine($"Surname  : {output.Surname}");
    }
}

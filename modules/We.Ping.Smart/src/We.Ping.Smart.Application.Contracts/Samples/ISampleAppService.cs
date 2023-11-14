using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace We.Ping.Smart.Samples;

public interface ISampleAppService : IApplicationService
{
    Task<SampleDto> GetAsync();

    Task<SampleDto> GetAuthorizedAsync();
}

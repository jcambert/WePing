using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace We.Ping.Girpe.Samples;

public interface ISampleAppService : IApplicationService
{
    Task<SampleDto> GetAsync();

    Task<SampleDto> GetAuthorizedAsync();
}

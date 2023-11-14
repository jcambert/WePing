using System.Threading.Tasks;
using Xunit;

namespace We.Ping.Smart.Samples;

public class SampleManager_Tests : SmartDomainTestBase
{
    //private readonly SampleManager _sampleManager;

    public SampleManager_Tests()
    {
        //_sampleManager = GetRequiredService<SampleManager>();
    }

    [Fact]
    public Task Method1Async()
    {
        return Task.CompletedTask;
    }
}

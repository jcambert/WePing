using MicroS_Common.Logging;
using MicroS_Common.Metrics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace WePing
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)

            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder
                .UseStartup<Startup>()
                .UseLogging()
                .UseAppMetrics(); ;
            });
    }
}

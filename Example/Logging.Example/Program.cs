namespace Logging.Example;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using NLog.Extensions.Logging;

class Program
{
    static Task Main(string[] args) =>
        CreateHostBuilder(args).Build().RunAsync();

    static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    //注入Nlog日志
                    logging.AddNLog("Cfgs/NLog.config");
                    logging.AddConsole();
                })
            .ConfigureServices((context, services) =>
                services.AddHostedService<Worker>());

}
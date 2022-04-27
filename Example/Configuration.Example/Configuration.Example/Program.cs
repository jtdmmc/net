using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Configuration.Example;
class Program
{
    static Task Main(string[] args) =>
        CreateHostBuilder(args).Build().RunAsync();

    static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
     // 注入配置
     .ConfigureAppConfiguration((hostingContext, configuration) =>
     {
         //情况配置程序提供程序
         configuration.Sources.Clear();
         IHostEnvironment env = hostingContext.HostingEnvironment;
         //添加配置文件
         configuration
             .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
             .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true);
         IConfigurationRoot configurationRoot = configuration.Build();
         TransientFaultHandlingOptions options = new();
         configurationRoot.GetSection(nameof(TransientFaultHandlingOptions))
                          .Bind(options);
         Console.WriteLine($"TransientFaultHandlingOptions.Enabled={options.Enabled}");
         Console.WriteLine($"TransientFaultHandlingOptions.AutoRetryDelay={options.AutoRetryDelay}");
     })
     // 注入服务
     .ConfigureServices((context, services) =>
     {      
         //将配置选项注入服务
         var configurationRoot = context.Configuration;
         services.Configure<TransientFaultHandlingOptions>(
             configurationRoot.GetSection(nameof(TransientFaultHandlingOptions)));
         services.AddHostedService<Worker>();
     }); 

    }
}

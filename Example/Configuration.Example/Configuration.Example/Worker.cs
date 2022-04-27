using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Configuration.Example;

public class Worker : BackgroundService
{
    //配置选项注入
    private readonly TransientFaultHandlingOptions _options;
    //日志注入
    private readonly ILogger<Worker> _logger;
    public Worker(IOptions<TransientFaultHandlingOptions> options,
        ILogger<Worker> logger)
    {
        _options = options.Value;
        _logger = logger;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Enabled:{},AutoRetryDelay:{}", _options.Enabled, _options.AutoRetryDelay);
            await Task.Delay(1000, stoppingToken);
        }
    }
}
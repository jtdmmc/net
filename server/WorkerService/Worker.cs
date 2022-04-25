using Autofac;
using DotnetSoftwarePlatform.Api.Server;
using log4net;

namespace DotnetSoftwarePlatform.WorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Factory.StartNew(() =>
            {
                LogManager.GetLogger("MVI.DotnetSoftwarePlatform");
                IocHelper.Setup();
                using (var scope = IocHelper.Container.BeginLifetimeScope())
                {
                    var server = IocHelper.Container.Resolve<IApiServer>();
                    server.Start();
                }
            },TaskCreationOptions.LongRunning);

        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            foreach (System.Diagnostics.Process thisProc in System.Diagnostics.Process.GetProcesses())
            {
                if (thisProc.ProcessName == "WorkerService")
                {
                    thisProc.Kill(); //当发送关闭窗口命令无效时强行结束进程                    
                }
            }
            return base.StopAsync(cancellationToken);
        }
    }
}
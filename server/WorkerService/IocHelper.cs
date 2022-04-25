using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using DotnetSoftwarePlatform.Api.Server;
using DotnetSoftwarePlatform.Api.Server.DtoMapper;
using DotnetSoftwarePlatform.Api.Server.Handlers;
using DotnetSoftwarePlatform.AutofacLogging;
using DotnetSoftwarePlatform.Common.Helpers;
using DotnetSoftwarePlatform.DAL.EF;
using DotnetSoftwarePlatform.DAL.EF.Services;
using DotnetSoftwarePlatform.Domain.Interfaces.AppServices;
using DotnetSoftwarePlatform.Domain.Interfaces.DataServices;
using DotnetSoftwarePlatform.Domain.Services;
using log4net;
using log4net.Config;
using MVI.DotnetSoftwarePlatform.Api.Thrift;


namespace DotnetSoftwarePlatform.WorkerService
{
    public static class IocHelper
    {
        public static IConfiguration Configuration { get; set; }
        private static Common.Interfaces.ILogger Logger;
        public static IContainer Container { get; set; }

        public static void Setup()
        {
            Configuration = new ConfigurationBuilder()
              .Add(new Microsoft.Extensions.Configuration.Json.JsonConfigurationSource { Path = "appsettings.json", ReloadOnChange = true })
              .Build();
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("DotnetSoftwarePlatform.log4net.config"));

            var builder = new ContainerBuilder();

            builder.RegisterType<DAL.EF.DotnetSoftwarePlatformContext>()
                    .WithParameter("sqlConnection", Configuration.GetConnectionString("SQLConnection"));
            builder.RegisterType<ServiceImpl>();

            builder.RegisterType<ThriftApiServer>().As<IApiServer>()
                .WithParameter("port", ServicesConstants.SERVER_PORT);
            builder.RegisterInstance<ILog>(LogManager.GetLogger("MVI.DotnetSoftwarePlatform"));
            builder.RegisterModule<LoggingModule>();
            builder.RegisterType<Log4NetLogFactory>().As<AutofacLogging.ILoggerFactory>().SingleInstance();
            builder.RegisterType<Log4NetAdapter>().As<Common.Interfaces.ILogger>().SingleInstance();

            builder.RegisterType<DtoMapper>().As<IDtoMapper>();

            builder.RegisterType<AccountService>().As<IAccountService>();

            builder.RegisterType<AccountDataProvider>().As<IAccountDataProvider>();
            builder.RegisterType<PowerDataProvider>().As<IPowerDataProvider>();
            builder.RegisterType<RoleDataProvider>().As<IRoleDataProvider>();

            Container = builder.Build();
            using (var client = new DotnetSoftwarePlatformContext())
            {
                if (client.Database.EnsureCreated())
                {
                    SeedInitalizer.Seed(client);
                }
            }
            Logger = Container.Resolve<Common.Interfaces.ILogger>();
            //Logger.Debug("test");
        }
    }
}

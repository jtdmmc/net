using DotnetSoftwarePlatform.Common.Interfaces;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetSoftwarePlatform.AutofacLogging
{
    public sealed class Log4NetLogFactory : ILoggerFactory
    {
        public ILogger GetLogger(string name)
        {
            var log = LogManager.GetLogger(name);
            return new Log4NetAdapter(log);
        }
    }
}

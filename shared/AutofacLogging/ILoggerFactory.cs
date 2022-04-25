using DotnetSoftwarePlatform.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetSoftwarePlatform.AutofacLogging
{
    public interface ILoggerFactory
    {
        ILogger GetLogger(string name);
    }
}

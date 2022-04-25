using DotnetSoftwarePlatform.Common.Helpers;
using DotnetSoftwarePlatform.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetSoftwarePlatform.Common
{
    public sealed class TimeMeasurer : IDisposable
    {
        private readonly string _name;
        private readonly ILogger _logger;
        private readonly Stopwatch _stopwatch;
        public TimeMeasurer(string name, ILogger logger)
        {
            _name = name;
            _logger = logger;
            _stopwatch = new Stopwatch();
            _logger.Debug($"TimeMeasurer Enter {_name}");
            _stopwatch.Start();
        }

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)。
                    _stopwatch.Stop();
                    if (_stopwatch.ElapsedMilliseconds > 500)
                    {
                        _logger.Debug($"Slow log: {_name}, cost: {_stopwatch.ElapsedMilliseconds} ms");
                    }
                    _logger.Debug($"TimeMeasurer Leave {_name}");
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。

                disposedValue = true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        // ~TimeMeasurer() {
        //   // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
        //   Dispose(false);
        // }

        // 添加此代码以正确实现可处置模式。
        public void Dispose()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}

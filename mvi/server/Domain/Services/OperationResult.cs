using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetSoftwarePlatform.Domain.Services
{
    public sealed class OperationResult
    {
        public static OperationResult Ok
        {
            get;
        } = new OperationResult(true);

        public static OperationResult UnknownError
        {
            get;
        } = new OperationResult(false, "操作失败，原因未知");

        public bool IsSuccess
        {
            get;
        }

        public string Message
        {
            get;
        }

        public OperationResult(bool success, string msg = "")
        {
            IsSuccess = success;
            Message = msg;
        }
    }
}

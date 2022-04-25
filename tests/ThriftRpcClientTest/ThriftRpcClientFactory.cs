using MVI.DotnetSoftwarePlatform.Api.Thrift;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thrift.Protocol;
using Thrift.Transport;

namespace ThriftRpcClientTest
{
    public class ThriftRpcClientFactory
    {
        private readonly TAccountService.Iface _accountService;


        public ThriftRpcClientFactory(string serverHost)
        {
            var sock = new TSocket(serverHost, ServicesConstants.SERVER_PORT);
            sock.TcpClient.NoDelay = false;
            TTransport transport = sock;
            transport = new TFramedTransport(transport);
            TProtocol protocol = new TCompactProtocol(transport);
            _accountService = new TAccountService.Client(new TMultiplexedProtocol(protocol, typeof(TAccountService).Name));
            transport.Open();
        }

        public TAccountService.Iface GetAccountService()
        {
            return _accountService;
        }
    }
}

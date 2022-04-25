using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thrift.Server;
using Thrift.Transport;
using Thrift.Protocol;
using MVI.DotnetSoftwarePlatform.Api.Thrift;
using Newtonsoft.Json;
using System.Net;
using DotnetSoftwarePlatform.Api.Server.Handlers;
using DotnetSoftwarePlatform.Common.Interfaces;

namespace DotnetSoftwarePlatform.Api.Server
{
    public class ThriftApiServer : IApiServer
    {

        private readonly ServiceImpl _handler;
        private readonly int _port;
        private readonly ILogger _logger;
        public ThriftApiServer(ServiceImpl handler, int port,ILogger logger)
        {
            _logger = logger;
            _handler = handler;
            _port = port;
        }

        public void Start()
        {

            TMultiplexedProcessor processor = new TMultiplexedProcessor();
            processor.RegisterProcessor(typeof(TAccountService).Name, new TAccountService.Processor(_handler));
            TServerTransport serverTransport = new TServerSocket(ServicesConstants.SERVER_PORT);
            TProtocolFactory protocolFactory = new TCompactProtocol.Factory();
            TTransportFactory transportFactory = new TFramedTransportEx.FactoryEx();

            // 为了保证我们的Context记录方案有效, 这里必须使用TThreadedServer。否则, 会出现混乱
            TServer server = new TThreadedServer(processor, serverTransport, transportFactory, protocolFactory);
            server.setEventHandler(new ThriftServerEventHandler(_handler,_logger));
            server.Serve();
        }

    }

    class ThriftServerEventHandler : TServerEventHandler
    {
        private readonly ServiceImpl _handler;
        private int _nextConnectionId = 1;
        private readonly ILogger _logger;
        public ThriftServerEventHandler(ServiceImpl service, ILogger logger)
        {
            _logger = logger;
            _handler = service;
        }

        // Called when a new client has connected and is about to being processing.
        public object createContext(TProtocol input, TProtocol output)
        {
            var clientHost = "";
            var clientPort = "";
            TSocket socket = null;
            if (input.Transport is TSocket ts)
            {
                socket = ts;
            }
            else if (input.Transport is TFramedTransportEx tft && tft.Socket is TSocket ts1)
            {
                socket = ts1;
            }
            if (socket != null)
            {
                if (!String.IsNullOrEmpty(socket.Host))
                {
                    clientHost = socket.Host;
                    clientPort = socket.Port.ToString();
                }
                else if (socket.TcpClient.Client.RemoteEndPoint is IPEndPoint pep)
                {
                    clientHost = pep.Address.ToString();
                    clientPort = pep.Port.ToString();
                }
            }
            var ctx = new ServerContext(_nextConnectionId++, clientHost, clientPort);
            //_logger.DebugFormat("TServerEventHandler.createContext - connection # {0} established. Info: {1}{2}",
            //    ctx.ConnectionId, Environment.NewLine, ctx.ToString());
            if (ctx.ClientIdentity.Length > 1)
            {
                // 有效的client id
                foreach (var item in _handler.ClientToThreadMap.ToArray())
                {
                    if (item.Value == ctx.ClientIdentity)
                    {
                        //try
                        //{
                        //    var res = _handler.ExitLoginWithIp(_handler.ClientToThreadMap[Thread.CurrentThread.ManagedThreadId]);
                        //    if (res.Message == "")
                        //        continue;
                        //    var exp = _handler.GetLastExperiment(res.Message);
                        //    //if (!exp.IsCompleted())
                        //    {
                        //        _handler.StopExperiment(exp.Id);
                        //    }

                        //}
                        //catch (Exception e)
                        //{
                        //    //_logger.Information($"Closed Experiment Failured : {e.Message}");
                        //}
                        //finally
                        //{ }
                        break;
                    }
                }
                _handler.ClientToThreadMap.TryAdd(Thread.CurrentThread.ManagedThreadId, ctx.ClientIdentity);
            }
            return ctx;
        }

        // Called when a client has finished request-handling to delete server context.
        public void deleteContext(object serverContext, TProtocol input, TProtocol output)
        {
            ServerContext ctx = serverContext as ServerContext;
            //TODO:
            try
            {
                //var res = _handler.ExitLoginWithIp(_handler.ClientToThreadMap[Thread.CurrentThread.ManagedThreadId]);
                //if (res.Message == "")
                //    return;
                //var exp = _handler.GetLastExperiment(res.Message);
                ////if(!exp.IsCompleted())
                //{
                //    _handler.StopExperiment(exp.Id);
                //}

            }
            catch (Exception e)
            {
                //_logger.Information($"Closed Experiment Failured : {e.Message}");
            }
            _handler.ClientToThreadMap.TryRemove(Thread.CurrentThread.ManagedThreadId, out _);
            //_logger.DebugFormat("TServerEventHandler.createContext - connection # {0} terminated", ctx?.ConnectionId);
        }

        // Called before the server begins.
        public void preServe()
        {
            //_logger.DebugFormat("TServerEventHandler.preServe - called only once before server starts accepting connections");
        }

        // Called when a client is about to call the processor.
        public void processContext(object serverContext, TTransport transport)
        {
            ServerContext ctx = serverContext as ServerContext;
            //_logger.DebugFormat("TServerEventHandler.createContext - connection # {0} is ready to process next request", ctx?.ConnectionId);
        }
    }

    sealed class ServerContext
    {
        public int ConnectionId
        {
            get;
        }

        public DateTime CreatedAt
        {
            get;
        }

        public string ClientIp
        {
            get;
        }

        public string ClientPort
        {
            get;
        }

        public string ClientIdentity
        {
            get
            {
                return $"{ClientIp}:{ClientPort}";
            }
        }

        public ServerContext(int connId, string cip, string cport)
        {
            ConnectionId = connId;
            CreatedAt = DateTime.Now;
            ClientIp = cip;
            ClientPort = cport;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    internal class TFramedTransportEx : TFramedTransport
    {
        public TSocket Socket { get; }

        public TFramedTransportEx(TTransport transport)
            : base(transport)
        {
            if (transport is TSocket s)
            {
                Socket = s;
            }
        }

        public class FactoryEx : Factory
        {
            public override TTransport GetTransport(TTransport trans)
            {
                return new TFramedTransportEx(trans);
            }
        }
    }
}

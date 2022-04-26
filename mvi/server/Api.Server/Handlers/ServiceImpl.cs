using DotnetSoftwarePlatform.Api.Server.DtoMapper;
using DotnetSoftwarePlatform.Common.Interfaces;
using DotnetSoftwarePlatform.Domain.Interfaces.AppServices;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetSoftwarePlatform.Api.Server.Handlers
{
    public sealed partial class ServiceImpl
    {
        private readonly IDtoMapper _mapper;
        private readonly ILogger _logger;
        private readonly ConcurrentDictionary<int, string> _clientToThreadMap;

        public ServiceImpl(ILogger logger,
            IDtoMapper mapper,
            IAccountService accountService)
        {
            _logger = logger;
            _mapper = mapper;
            _accountService = accountService;
            _clientToThreadMap = new ConcurrentDictionary<int, string>();
        }

        public ConcurrentDictionary<int, string> ClientToThreadMap
        {
            get { return _clientToThreadMap; }
        }

    }
}

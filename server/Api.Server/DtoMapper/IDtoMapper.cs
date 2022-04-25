using DotnetSoftwarePlatform.Domain.Model.Entities;
using DotnetSoftwarePlatform.Domain.Services;
using MVI.DotnetSoftwarePlatform.Api.Thrift;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetSoftwarePlatform.Api.Server.DtoMapper
{
    public interface IDtoMapper
    {
        TCommonReply ToDto(OperationResult result);
        TPageAccount ToDto(PageAccountResult result);
        TAccount ToDto(User account);
        User FromDto(TAccount dto);
        TRole ToDto(Role role);
        Role FromDto(TRole dto);
        Power FromDto(TPower power);
        TPower ToDto(Power power);
    }
}

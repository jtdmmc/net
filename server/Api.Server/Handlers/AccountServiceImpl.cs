using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotnetSoftwarePlatform.Common.Helpers;
using DotnetSoftwarePlatform.Domain.Interfaces.AppServices;
using MVI.DotnetSoftwarePlatform.Api.Thrift;

namespace DotnetSoftwarePlatform.Api.Server.Handlers
{
    public sealed partial class ServiceImpl : TAccountService.Iface
    {
        private readonly IAccountService _accountService;
        public TCommonReply SignInWithAccount(string uniqueName, string password)
        {
            var result = _accountService.SignInAsync(uniqueName,password).Result;
            return _mapper.ToDto(result);
        }
        public TCommonReply UpdatePassword(string uniqueName, string oldPassword, string newPassword)
        {
            var result = _accountService.UpdatePasswordAsync(uniqueName, oldPassword, newPassword).Result;
            return _mapper.ToDto(result);
        }

        public TAccount GetAccountByUniqueName(string uniqueName)
        {
            try
            {
                var account = _accountService.GetByUniqueNameAsync(uniqueName).Result;
                return _mapper.ToDto(account);
            }
            catch (Exception e)
            {
                _logger.Error(e);
                throw new TUnknownErrorException();
            }
        }

        public TCommonReply AddAccount(TAccount account)
        {
            var user = _mapper.FromDto(account);
            if(user == null)
            {
                return new TCommonReply { IsSuccess = false,Message = "空用户"};
            }
            var result = _accountService.AddAccountAsync(user).Result;
            return _mapper.ToDto(result);
        }


        public TCommonReply DelAccount(string uniqueName)
        {
            var result = _accountService.DeleteAccountAsync(uniqueName).Result;
            return _mapper.ToDto(result);
        }
      
        public TPageAccount GetAccountsByPage(int pageIndex, int pageSize)
        {
            var result = _accountService.GetAccountsByPage(pageIndex, pageSize).Result;
            return _mapper.ToDto(result);
        }

        public List<TAccount> GetAllAccounts()
        {
            var result = _accountService.GetAllAccounts().Result;
            return result.Select(o => _mapper.ToDto(o)).ToList();
        }

        public TCommonReply UpdateAccount(TAccount account)
        {
            var user = _mapper.FromDto(account);
            if (user == null)
            {
                return new TCommonReply { IsSuccess = false, Message = "空用户" };
            }
            var result = _accountService.UpdateAccountAsync(user).Result;
            return _mapper.ToDto(result);
        }

        public TCommonReply AddRole(TRole dto)
        {
            var role = _mapper.FromDto(dto);
            if (role == null)
            {
                return new TCommonReply { IsSuccess = false, Message = "空角色" };
            }
            var result = _accountService.AddRoleAsync(role).Result;
            return _mapper.ToDto(result);
        }
        public TCommonReply DelRole(int roleId)
        {
            var result = _accountService.DeleteRoleAsync(roleId).Result;
            return _mapper.ToDto(result);
        }

        public List<TRole> GetAllRoles()
        {
            var result = _accountService.GetAllRoles().Result;
            return result.Select(o => _mapper.ToDto(o)).ToList();
        }

        public TCommonReply UpdateRole(TRole dto)
        {
            var role = _mapper.FromDto(dto);
            if (role == null)
            {
                return new TCommonReply { IsSuccess = false, Message = "空角色" };
            }
            var result = _accountService.UpdateRoleAsync(role).Result;
            return _mapper.ToDto(result);
        }

        public List<TPower> GetAllPowers()
        {
            var result = _accountService.GetAllPowers().Result;
            return result.Select(o => _mapper.ToDto(o)).ToList();
        }
    }
}

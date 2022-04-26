using DotnetSoftwarePlatform.Common.Helpers;
using DotnetSoftwarePlatform.Common.Interfaces;
using DotnetSoftwarePlatform.Domain.Interfaces.AppServices;
using DotnetSoftwarePlatform.Domain.Interfaces.DataServices;
using DotnetSoftwarePlatform.Domain.Model.Entities;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetSoftwarePlatform.Domain.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountDataProvider _accountDataProvider;
        private readonly IRoleDataProvider _roleDataProvider;
        private readonly IPowerDataProvider _powerDataProvider;
        private readonly ILogger _logger;

        public AccountService(IAccountDataProvider dataProvider,
            IRoleDataProvider roleDataProvider,
            IPowerDataProvider powerDataProvider,
            ILogger logger)
        {
            _roleDataProvider = roleDataProvider;
            _powerDataProvider = powerDataProvider; 
            _accountDataProvider = dataProvider;
            _logger = logger;
        }

        public async Task<OperationResult> UpdatePasswordAsync(string uniqueName, string oldPassword, string newPassword)
        {
            return await _accountDataProvider.UpdatePasswordAsync(uniqueName, oldPassword, newPassword);
        }

        public async Task<OperationResult> SignInAsync(string uniqueName, string password)
        {
            var account = await GetByUniqueNameAsync(uniqueName);
            if (account == null)
            {
                return new OperationResult(false, "用户不存在");
            }
            var ph = new PasswordHash(account.PasswordHash);
            if (ph.Verify(password))
            {
                _logger.InformationFormat("用户【{0}】登录成功", uniqueName);
                return OperationResult.Ok;
            }
            else
            {
                return new OperationResult(false, "密码错误");
            }
        }

        public async Task<User> GetByUniqueNameAsync(string uniqueName)
        {
            return await _accountDataProvider.GetByUniqueNameAsync(uniqueName);
        }

        public Task<OperationResult> AddAccountAsync(User account)
        {
            return _accountDataProvider.AddAccountAsync(account);
        }

        public Task<OperationResult> DeleteAccountAsync(string uniqueName)
        {
            return _accountDataProvider.DeleteAccountAsync(uniqueName);
        }

        public Task<IEnumerable<User>> GetAllAccounts()
        {
            return _accountDataProvider.GetAllAccounts();
        }

        public Task<OperationResult> UpdateAccountAsync(User account)
        {
            return _accountDataProvider.UpdateAccountAsync(account);
        }

        public Task<PageAccountResult> GetAccountsByPage(int pageIndex, int pageSize)
        {
            return _accountDataProvider.GetAccountsByPage(pageIndex, pageSize);
        }

        public Task<OperationResult> AddRoleAsync(Role role)
        {
            return  _roleDataProvider.AddRoleAsync(role);
        }
        public Task<OperationResult> DeleteRoleAsync(int id)
        {
            return _roleDataProvider.DeleteRoleAsync(id);
        }

        public Task<IEnumerable<Role>> GetAllRoles()
        {
            return _roleDataProvider.GetAllRoles();
        }

        public Task<OperationResult> UpdateRoleAsync(Role role)
        {
            return _roleDataProvider.UpdateRoleAsync(role);
        }

        public Task<IEnumerable<Power>> GetAllPowers()
        {
            return _powerDataProvider.GetAllPowers();
        }
    }
}

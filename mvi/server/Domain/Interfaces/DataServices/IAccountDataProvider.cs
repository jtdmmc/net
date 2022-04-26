using DotnetSoftwarePlatform.Domain.Model.Entities;
using DotnetSoftwarePlatform.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetSoftwarePlatform.Domain.Interfaces.DataServices
{
    public interface IAccountDataProvider
    {
        Task<User> GetByUniqueNameAsync(string uniqueName);
        Task<OperationResult> UpdatePasswordAsync(string uniqueName, string oldPassword, string newPassword);
        Task<OperationResult> AddAccountAsync(User account);
        Task<OperationResult> UpdateAccountAsync(User account);
        Task<OperationResult> DeleteAccountAsync(string uniqueName);
        Task<IEnumerable<User>> GetAllAccounts();
        Task<PageAccountResult> GetAccountsByPage(int pageIndex, int pageSize);
    }
}

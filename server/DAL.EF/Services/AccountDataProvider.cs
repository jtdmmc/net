using DotnetSoftwarePlatform.Common.Helpers;
using DotnetSoftwarePlatform.Domain.Interfaces.DataServices;
using DotnetSoftwarePlatform.Domain.Model.Entities;
using DotnetSoftwarePlatform.Domain.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetSoftwarePlatform.DAL.EF.Services
{
    public sealed class AccountDataProvider : IAccountDataProvider
    {
        private byte[] ComputePasswordHash(string password)
        {
            if (String.IsNullOrWhiteSpace(password))
            {
                return null;
            }
            PasswordHash ph = new PasswordHash(password);
            return ph.ToArray();
        }

        public async Task<User> GetByUniqueNameAsync(string uniqueName)
        {
            using (var db = new DotnetSoftwarePlatformContext())
            {
                var record = await db.Users.Where(x => x.UserName == uniqueName)
                    .Include(x => x.UserRoles).
                    ThenInclude(y => y.Role).
                    ThenInclude(z => z.RolePowers).
                    ThenInclude(s => s.Power)
                    .FirstOrDefaultAsync();
                return record;
            }
        }

        public async Task<User> GetByUniqueId(int id)
        {
            using (var db = new DotnetSoftwarePlatformContext())
            {
                var record = await db.Users.Where(x => x.Id == id)
                    .Include(x => x.UserRoles).
                    ThenInclude(y => y.Role).
                    ThenInclude(z => z.RolePowers).
                    ThenInclude(s => s.Power)
                    .FirstOrDefaultAsync();
                return record;
            }
        }

        public async Task<OperationResult> UpdatePasswordAsync(string uniqueName, string oldPassword, string newPassword)
        {

            using (var db = new DotnetSoftwarePlatformContext())
            {

                var entity = await db.Users.Where(x => x.UserName == uniqueName).Include(x => x.UserRoles).FirstOrDefaultAsync(); ;
                if (entity == null)
                {
                    return new OperationResult(false, "用户不存在");
                }
                var ph = new PasswordHash(entity.PasswordHash);
                if (ph.Verify(oldPassword) && newPassword != oldPassword)
                {
                    entity.PasswordHash = ComputePasswordHash(newPassword);
                    db.Users.Attach(entity).Property(x => x.PasswordHash).IsModified = true;
                    await db.SaveChangesAsync();
                    return OperationResult.Ok;
                }
                else if (newPassword == oldPassword)
                {
                    return new OperationResult(false, "新密码不能和旧密码一致");
                }
                else
                {
                    return new OperationResult(false, "旧密码错误");
                }
            }
        }

        public async Task<OperationResult> AddAccountAsync(User account)
        {
            using (var db = new DotnetSoftwarePlatformContext())
            {
                if (await db.Users.AnyAsync(x => x.UserName == account.UserName ))
                {
                    return new OperationResult(false, "已经存在同名的用户");
                }
                string ph = Encoding.Default.GetString(account.PasswordHash);
                account.PasswordHash = new PasswordHash(ph).ToArray();
                var result = db.Users.Add(account);
                await db.SaveChangesAsync();
                return OperationResult.Ok;
            }
        }

        public async Task<OperationResult> UpdateAccountAsync(User account)
        {
            using (var db = new DotnetSoftwarePlatformContext())
            {
                var result = await db.Users.Where(x => x.UserName == account.UserName)
                   .Include(x => x.UserRoles)
                   .FirstOrDefaultAsync();
                if (result != null)
                {
                    result.Remark = account.Remark;
                    result.UpdatedAt = account.UpdatedAt;   
                    result.IsEnabled = account.IsEnabled;
                    var entity = db.Users.Attach(result);
                    entity.Property(x => x.UpdatedAt).IsModified = true;
                    entity.Property(x => x.IsEnabled).IsModified = true;
                    entity.Property(x => x.Remark).IsModified = true;

                    db.TryUpdateManyToMany(result.UserRoles, account.UserRoles
                    .Select(x => new UserRole
                    {
                        UserId = result.Id,
                        RoleId = x.RoleId
                    }), x => x.RoleId);
                    await db.SaveChangesAsync();
                    return OperationResult.Ok;
                }
                return new OperationResult(false, "用户不存在");
            }
        }

        public async Task<OperationResult> DeleteAccountAsync(string uniqueName)
        {
            using (var db = new DotnetSoftwarePlatformContext())
            {
                var result = await db.Users.Where(x => x.UserName == uniqueName)
                   .Include(x => x.UserRoles)
                   .FirstOrDefaultAsync();
                if (result != null)
                {
                    db.Users.Remove(result);
                    await db.SaveChangesAsync();
                    return OperationResult.Ok;
                }
                return new OperationResult(false, "用户不存在");
            }
        }

        public async Task<IEnumerable<User>> GetAllAccounts()
        {
            using (var db = new DotnetSoftwarePlatformContext())
            {
                return await db.Users.Where(o => true).Include(x => x.UserRoles).ThenInclude(y => y.Role).
                    ThenInclude(z => z.RolePowers).
                    ThenInclude(s => s.Power).ToListAsync();
            }
        }

        public async Task<PageAccountResult> GetAccountsByPage(int pageIndex, int pageSize)
        {
            using (var db = new DotnetSoftwarePlatformContext())
            {
                var paged = await db.Users.GetPaged(pageIndex, pageSize);
                var list = new List<User>();
                foreach(var account in paged.Results)
                {
                    list.Add(await GetByUniqueId(account.Id));
                }
                return new PageAccountResult(paged.RowCount, paged.PageSize, paged.CurrentPage, list);
            }
        }
    }
}

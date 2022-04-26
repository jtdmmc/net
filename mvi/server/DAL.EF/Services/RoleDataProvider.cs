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
    public class RoleDataProvider : IRoleDataProvider
    {
        public async Task<OperationResult> AddRoleAsync(Role role)
        {
            using (var db = new DotnetSoftwarePlatformContext())
            {
                if (await db.Roles.AnyAsync(x => x.RoleName == role.RoleName))
                {
                    return new OperationResult(false, "已经存在同名的角色");
                }
                var result = db.Roles.Add(role);
                await db.SaveChangesAsync();
                return OperationResult.Ok;
            }
        }

        public async Task<OperationResult> DeleteRoleAsync(int id)
        {
            using (var db = new DotnetSoftwarePlatformContext())
            {
                var result = await db.Roles.Where(x => x.Id == id)
                   .Include(x => x.RolePowers)
                   .FirstOrDefaultAsync();
                if (result != null)
                {
                    db.Roles.Remove(result);
                    await db.SaveChangesAsync();
                    return OperationResult.Ok;
                }
                return new OperationResult(false, "角色不存在");
            }
        }

        public async Task<IEnumerable<Role>> GetAllRoles()
        {
            using (var db = new DotnetSoftwarePlatformContext())
            {
                return await db.Roles.Where(o => true).Include(x => x.RolePowers).ThenInclude(s => s.Power).ToListAsync();
            }
        }

        public async Task<OperationResult> UpdateRoleAsync(Role role)
        {
            using (var db = new DotnetSoftwarePlatformContext())
            {
                var result = await db.Roles.Where(x => x.Id == role.Id)
                   .Include(x => x.RolePowers)
                   .FirstOrDefaultAsync();
                if (result != null)
                {
                    result.RoleName = role.RoleName;
                    db.Roles.Attach(result).Property(x => x.RoleName).IsModified = true;
                    db.TryUpdateManyToMany(result.RolePowers, role.RolePowers
                   .Select(x => new RolePower
                   {
                       PowerId = x.PowerId,
                       RoleId = result.Id
                   }), x => x.PowerId);
                    await db.SaveChangesAsync();
                    return OperationResult.Ok;
                }
                return new OperationResult(false, "角色不存在");
            }
        }
    }
}

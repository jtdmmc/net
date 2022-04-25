using DotnetSoftwarePlatform.Domain.Model.Entities;
using DotnetSoftwarePlatform.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetSoftwarePlatform.Domain.Interfaces.DataServices
{
    public interface IRoleDataProvider
    {
        Task<OperationResult> AddRoleAsync(Role role);
        Task<OperationResult> DeleteRoleAsync(int id);
        Task<IEnumerable<Role>> GetAllRoles();
        Task<OperationResult> UpdateRoleAsync(Role role);
    }
}

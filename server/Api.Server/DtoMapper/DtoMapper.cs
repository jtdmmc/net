using DotnetSoftwarePlatform.Domain.Model.Entities;
using DotnetSoftwarePlatform.Domain.Services;
using MVI.DotnetSoftwarePlatform.Api.Thrift;
using DotnetSoftwarePlatform.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetSoftwarePlatform.Api.Server.DtoMapper
{
    public class DtoMapper : IDtoMapper
    {
        public TCommonReply ToDto(OperationResult result)
        {
            return new TCommonReply()
            {
                IsSuccess = result.IsSuccess,
                Message = result.Message,
            };
        }

        public TPageAccount ToDto(PageAccountResult result)
        {
            return new TPageAccount()
            {
                PageSize = result.PageSize,
                CurrPage = result.CurrPage,
                TotalCount = result.TotalCount,
                Data = result.Data.Select(x => ToDto(x)).ToList(),
            };
        }

        public TAccount ToDto(User account)
        {
            if (account == null)
            {
                return null;
            }
            var dto = new TAccount()
            {
                UniqueName = account.UserName,
                UpdatedAt = DateTimeExtension.DateTimeToTick(account.UpdatedAt),
                CreatedAt = DateTimeExtension.DateTimeToTick(account.CreatedAt),
                Remark = account.Remark,
                IsEnabled = account.IsEnabled,
                Roles = account.UserRoles?.Select(o => new TRole {
                    RoleId = o.RoleId,
                    RoleName = o.Role?.RoleName,
                    Powers = o.Role?.RolePowers?.Select(x => new TPower {
                        PowerId = x.PowerId,
                        PowerName = x.Power?.PowerName
                    }).ToList(),
                }).ToList(),
            };
            return dto;
        }

        public User FromDto(TAccount dto)
        {
            if (dto == null)
            {
                return null;
            }
            var account = new User()
            {
                UserName = dto.UniqueName,
                UpdatedAt = DateTimeExtension.TickToDateTime(dto.UpdatedAt),
                CreatedAt = DateTimeExtension.TickToDateTime(dto.CreatedAt),
                Remark = dto.Remark,
                IsEnabled = dto.IsEnabled,
                UserRoles = new List<UserRole>(),
            };
            if(!string.IsNullOrEmpty(dto.Password))
            {
                account.PasswordHash = Encoding.Default.GetBytes(dto.Password);
            }
            if(dto.Roles != null && dto.Roles.Count > 0)
            {
                account.UserRoles = dto.Roles?.Select(x => new UserRole
                {
                    RoleId = x.RoleId,
                    User = account,
                    UserId = account.Id,
                }).ToList();
            }
            return account;
        }

        public TRole ToDto(Role role)
        {
            if(role == null)
            {
                return null;
            }
            var dto = new TRole()
            {
                //Powers = role.Powers.Select(o => ToDto(o)).ToList(),
                RoleId = role.Id,
                RoleName = role.RoleName,
                Powers = role.RolePowers?.Select(o => new TPower { PowerId = o.PowerId,PowerName = o.Power?.PowerName}).ToList(),
            };
            return dto;

        }

        public Role FromDto(TRole dto)
        {
            if (dto == null)
            {
                return null;
            }
            var role = new Role()
            {
                RolePowers = new List<RolePower>(),
                RoleName = dto.RoleName,
                Id = dto.RoleId,
            };
            if(dto.Powers != null && dto.Powers.Count > 0)
            {
                role.RolePowers = dto.Powers?.Select(o => new RolePower {
                    PowerId = o.PowerId,
                    RoleId = dto.RoleId ,
                }).ToList();
            }
            return role;
        }

        public Power FromDto(TPower dto)
        {
            if (dto == null)
            {
                return null;
            }
            var power = new Power()
            {
                PowerName = dto.PowerName,
                Id = dto.PowerId,
            };
            return power;
        }

        public TPower ToDto(Power power)
        {
            if (power == null)
            {
                return null;
            }
            var dto = new TPower()
            {
                PowerName = power.PowerName,
                PowerId = power.Id,
            };
            return dto;
        }
    }
}

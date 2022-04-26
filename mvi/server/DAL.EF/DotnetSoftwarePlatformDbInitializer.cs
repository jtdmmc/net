using DotnetSoftwarePlatform.Domain.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetSoftwarePlatform.DAL.EF
{
    public static class SeedInitalizer
    {
        public static void Seed(DotnetSoftwarePlatformContext context)
        {
            var power1 = new Power { PowerName = "数据导出" };
            var power2 = new Power { PowerName = "人工复检" };
            var power3 = new Power { PowerName = "模型管理" };
            var power4 = new Power { PowerName = "用户日志" };
            var power5 = new Power { PowerName = "账号管理" };
            context.Powers.Add(power1);
            context.Powers.Add(power2);
            context.Powers.Add(power3);
            context.Powers.Add(power4);
            context.Powers.Add(power5);

            context.Roles.Add(new Role { RoleName = "管理员"});
            context.Roles.Add(new Role { RoleName = "生产" });
            context.Roles.Add(new Role { RoleName = "生技" });
            context.Roles.Add(new Role { RoleName = "工技保全" });

            var user = new User()
            {
                UserName = "admin",
                PasswordHash =new Common.Helpers.PasswordHash("123456").ToArray(),
            };
            context.Users.Add(user);
         

            context.SaveChanges();

            context.RolePowers.Add(new RolePower { RoleId = 1, PowerId = 1 });
            context.RolePowers.Add(new RolePower { RoleId = 1, PowerId = 2 });
            context.RolePowers.Add(new RolePower { RoleId = 1, PowerId = 3 });
            context.RolePowers.Add(new RolePower { RoleId = 1, PowerId = 4 });
            context.RolePowers.Add(new RolePower { RoleId = 1, PowerId = 5 });

            context.UserRoles.Add(new UserRole { RoleId = 1, UserId = 1 });
            context.SaveChanges();
        }
    }
}

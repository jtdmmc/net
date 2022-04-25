using DotnetSoftwarePlatform.Common.Helpers;
using MVI.DotnetSoftwarePlatform.Api.Thrift;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThriftRpcClientTest
{
    internal class Program
    {

        static void TestSignIn(TAccountService.Iface accountService)
        {
            var res = accountService.SignInWithAccount("admin", "123456");
            Console.WriteLine(res.Message);
        }

        static void TestUpdatePassword(TAccountService.Iface accountService)
        {
            var res = accountService.UpdatePassword("admin", "12345678","123456");
            Console.WriteLine(res.Message);
            res = accountService.SignInWithAccount("admin", "123456");
            Console.WriteLine(res.Message);
        }

        static void TestAddAccount(TAccountService.Iface accountService)
        {
            var res = accountService.AddAccount(new TAccount
            {
                UniqueName = "admin7",
                Password = "123456",
                Roles = new List<TRole>
                { 
                    new TRole { RoleId = 2},
                }
            });
            Console.WriteLine(res.Message);
            res = accountService.SignInWithAccount("admin7", "123456");
            Console.WriteLine(res.Message);
        }

        static void TestRemoveAccout(TAccountService.Iface accountService)
        {
            TestAddAccount(accountService);
            var res = accountService.DelAccount("admin1");
            Console.WriteLine(res.Message);
        }

        static void TestGetAllAccouts(TAccountService.Iface accountService)
        {
            var res = accountService.GetAllAccounts();
        }

        static void TestUpdateAccount(TAccountService.Iface accountService)
        {
            var user = accountService.GetAccountByUniqueName("admin");
            user.Remark = "aaa";
            user.Password = "22222";
            user.UpdatedAt = DateTimeExtension.DateTimeToTick(DateTime.Now);
            user.Roles.Add(new TRole { RoleId = 2,RoleName = "  生产"  });
            var res = accountService.UpdateAccount(user);
            res = accountService.SignInWithAccount("admin", "123456");
            Console.WriteLine(res.Message);
        }

        static void TestGetAccountsByPage(TAccountService.Iface accountService)
        {
            var res = accountService.GetAccountsByPage(0, 10);
        }

        static void TestGetAllPowers(TAccountService.Iface accountService)
        {
            var res = accountService.GetAllPowers();
        }

        static void TestGetAllRoles(TAccountService.Iface accountService)
        {
            var res = accountService.GetAllRoles();
        }

        static void TestAddRole(TAccountService.Iface accountService)
        {
            var res = accountService.AddRole(new TRole { RoleName = "test角色",Powers = new List<TPower> { new TPower { PowerId = 1 , PowerName = "数据导出" } } });
        }

        static void TestRemoveRole(TAccountService.Iface accountService)
        {
            var res = accountService.DelRole(1);
        }

        static void TestUpdateRole(TAccountService.Iface accountService)
        {
            var res = accountService.UpdateRole(new TRole {RoleId = 5, RoleName = "test角色2", Powers = new List<TPower> { new TPower { PowerId = 2, PowerName = "人工复检" } } });
        }


        static void Main(string[] args)
        {
            var thrift = new ThriftRpcClientFactory("127.0.0.1");
            //TestSignIn(thrift.GetAccountService());
            //TestUpdatePassword(thrift.GetAccountService());
            TestAddAccount(thrift.GetAccountService());
            //TestRemoveAccout(thrift.GetAccountService());
            //TestGetAllAccouts(thrift.GetAccountService());
            //TestUpdateAccount(thrift.GetAccountService());
            //TestGetAccountsByPage(thrift.GetAccountService());
            //TestGetAllPowers(thrift.GetAccountService());
            //TestGetAllRoles(thrift.GetAccountService());
            //TestAddRole(thrift.GetAccountService());
            //TestRemoveRole(thrift.GetAccountService());
            //TestUpdateRole(thrift.GetAccountService());
            System.Console.ReadKey();
        }
    }
}

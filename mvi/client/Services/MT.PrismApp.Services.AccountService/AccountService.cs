using log4net;
using MT.PrismApp.Common.Consts;
using MT.PrismApp.Services.Interfaces;
using MVI.DotnetSoftwarePlatform.Api.Thrift;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thrift.Protocol;
using Thrift.Transport;

namespace MT.PrismApp.Services.AccountService
{
    public class AccountService : IAccountService
    {
        public static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #region thrift client
        private TAccountService.Iface _accountServiceClient;
        public bool CreateAccountServiceClient(string serverHost = ServerInfo.IPAddresss)
        {
            try
            {

                if (_accountServiceClient == null)
                {
                    var sock = new TSocket(serverHost, ServicesConstants.SERVER_PORT);
                    sock.TcpClient.NoDelay = false;
                    TTransport transport = sock;
                    transport = new TFramedTransport(transport);
                    TProtocol protocol = new TCompactProtocol(transport);
                    _accountServiceClient = new TAccountService.Client(new TMultiplexedProtocol(protocol, typeof(TAccountService).Name));
                    transport.Open();
                }


                if (_accountServiceClient == null)
                {
                    Log.Error("创建TAccountService服务失败");
                    return false;
                }
            }
            catch (Exception e)
            {
                Log.Error("创建TAccountService服务失败");
                Log.Error(e.ToString());
                _accountServiceClient = null;
                return false;
            }
            return true;
        }
        #endregion
        TCommonReply TAccountService.ISync.SignInWithAccount(string uniqueName, string password)
        {
            try
            {
                if (CreateAccountServiceClient())
                {
                    var res = _accountServiceClient.SignInWithAccount(uniqueName, password);
                    Log.Info($"IsSuccess：{res.IsSuccess}；Message：{res.Message}");
                    return res;
                }

                return new TCommonReply() { IsSuccess = false, Message = ErrorInfo.Error_AccessingServer };
            }
            catch (Exception e)
            {
                Log.Error("Login 失败，" + e.ToString());
                return new TCommonReply() { IsSuccess = false, Message = ErrorInfo.Error_AccessingServer };
            }
        }

        TCommonReply TAccountService.ISync.UpdatePassword(string uniqueName, string oldPassword, string newPassword)
        {
            try
            {
                if (CreateAccountServiceClient())
                {
                    var res = _accountServiceClient.UpdatePassword(uniqueName, oldPassword, newPassword);
                    Log.Info($"IsSuccess：{res.IsSuccess}；Message：{res.Message}");
                    return res;
                }
                return new TCommonReply() { IsSuccess = false, Message = ErrorInfo.Error_AccessingServer };
            }
            catch (Exception e)
            {
                Log.Error("操作失败，" + e.ToString());
                return new TCommonReply() { IsSuccess = false, Message = ErrorInfo.Error_AccessingServer };
            }
        }

        TAccount TAccountService.ISync.GetAccountByUniqueName(string uniqueName)
        {
            try
            {
                if (CreateAccountServiceClient())
                {
                    var account = _accountServiceClient.GetAccountByUniqueName(uniqueName);
                    Log.Info(account);
                    return account;
                }
                return new TAccount();

            }
            catch (Exception e)
            {
                Log.Error("操作失败，" + e.ToString());
                return new TAccount();
            }
        }

        TCommonReply TAccountService.ISync.AddAccount(TAccount account)
        {
            try
            {
                if (CreateAccountServiceClient())
                {
                    var res = _accountServiceClient.AddAccount(account);
                    Log.Info($"IsSuccess：{res.IsSuccess}；Message：{res.Message}");
                    return res;
                }
                return new TCommonReply() { IsSuccess = false, Message = ErrorInfo.Error_AccessingServer };

            }
            catch (Exception e)
            {
                Log.Error("操作失败，" + e.ToString());
                return new TCommonReply() { IsSuccess = false, Message = e.ToString() };
            }
        }

        TCommonReply TAccountService.ISync.DelAccount(string uniqueName)
        {
            try
            {
                if (CreateAccountServiceClient())
                {
                    var res = _accountServiceClient.DelAccount(uniqueName);
                    Log.Info($"DelAccount:{uniqueName},IsSuccess：{res.IsSuccess}；Message：{res.Message}");
                    return res;
                }
                return new TCommonReply() { IsSuccess = false, Message = ErrorInfo.Error_AccessingServer };

            }
            catch (Exception e)
            {
                Log.Error($"DelAccount :{uniqueName},error:{  e.ToString()}");
                return new TCommonReply() { IsSuccess = false, Message = e.ToString() };
            }
        }

        TCommonReply TAccountService.ISync.UpdateAccount(TAccount account)
        {
            try
            {
                if (CreateAccountServiceClient())
                {
                    var res = _accountServiceClient.UpdateAccount(account);
                    Log.Info($"IsSuccess：{res.IsSuccess}；Message：{res.Message}");
                    return res;
                }
                return new TCommonReply() { IsSuccess = false, Message = ErrorInfo.Error_AccessingServer };

            }
            catch (Exception e)
            {
                Log.Error("操作失败，" + e.ToString());
                return new TCommonReply() { IsSuccess = false, Message = e.ToString() };
            }
        }

        List<TAccount> TAccountService.ISync.GetAllAccounts()
        {
            try
            {
                if (CreateAccountServiceClient())
                {
                    var accounts = _accountServiceClient.GetAllAccounts();
                    Log.Info(accounts);
                    return accounts;
                }
                return new List<TAccount>();

            }
            catch (Exception e)
            {
                Log.Error("操作失败，" + e.ToString());
                return new List<TAccount>();
            }
        }

        TPageAccount TAccountService.ISync.GetAccountsByPage(int pageIndex, int pageSize)
        {
            try
            {
                if (CreateAccountServiceClient())
                {
                    var accounts = _accountServiceClient.GetAccountsByPage(pageIndex, pageSize);
                    Log.Info(accounts);
                    return accounts;
                }
                return new TPageAccount();

            }
            catch (Exception e)
            {
                Log.Error("操作失败，" + e.ToString());
                return new TPageAccount();
            }
        }

        TCommonReply TAccountService.ISync.AddRole(TRole role)
        {
            try
            {
                if (CreateAccountServiceClient())
                {
                    var res = _accountServiceClient.AddRole(role);
                    Log.Info($"IsSuccess：{res.IsSuccess}；Message：{res.Message}");
                    return res;
                }
                return new TCommonReply() { IsSuccess = false, Message = ErrorInfo.Error_AccessingServer };

            }
            catch (Exception e)
            {
                Log.Error("操作失败，" + e.ToString());
                return new TCommonReply() { IsSuccess = false, Message = e.ToString() };
            }
        }

        TCommonReply TAccountService.ISync.DelRole(int roleId)
        {
            try
            {
                if (CreateAccountServiceClient())
                {
                    var res = _accountServiceClient.DelRole(roleId);
                    Log.Info($"IsSuccess：{res.IsSuccess}；Message：{res.Message}");
                    return res;
                }
                return new TCommonReply() { IsSuccess = false, Message = ErrorInfo.Error_AccessingServer };

            }
            catch (Exception e)
            {
                Log.Error("操作失败，" + e.ToString());
                return new TCommonReply() { IsSuccess = false, Message = e.ToString() };
            }
        }

        TCommonReply TAccountService.ISync.UpdateRole(TRole role)
        {
            try
            {
                if (CreateAccountServiceClient())
                {
                    var res = _accountServiceClient.UpdateRole(role);
                    Log.Info($"IsSuccess：{res.IsSuccess}；Message：{res.Message}");
                    return res;
                }
                return new TCommonReply() { IsSuccess = false, Message = ErrorInfo.Error_AccessingServer };

            }
            catch (Exception e)
            {
                Log.Error("操作失败，" + e.ToString());
                return new TCommonReply() { IsSuccess = false, Message = e.ToString() };
            }
        }

        List<TRole> TAccountService.ISync.GetAllRoles()
        {
            try
            {
                if (CreateAccountServiceClient())
                {
                    var roles = _accountServiceClient.GetAllRoles();
                    Log.Info(roles);
                    return roles;
                }
                else
                {
                    return new List<TRole>();
                }

            }
            catch (Exception e)
            {
                Log.Error("操作失败，" + e.ToString());
                return new List<TRole>();
            }
        }

        public List<TPower> GetAllPowers()
        {
            try
            {
                if (CreateAccountServiceClient())
                {
                    var powers = _accountServiceClient.GetAllPowers();
                    Log.Info(powers);
                    return powers;
                }
                else
                {
                    return new List<TPower>();
                }

            }
            catch (Exception e)
            {
                Log.Error("操作失败，" + e.ToString());
                return new List<TPower>();
            }
        }
    }
}

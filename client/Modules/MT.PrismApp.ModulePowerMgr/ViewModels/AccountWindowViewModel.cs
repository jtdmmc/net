using HandyControl.Data;
using MT.PrismApp.Common.Models;
using MT.PrismApp.ModulePowerMgr.Views;
using MT.PrismApp.Services.Interfaces;
using MVI.DotnetSoftwarePlatform.Api.Thrift;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MT.PrismApp.ModulePowerMgr.ViewModels
{
    public class AccountWindowViewModel : BindableBase
    {

        public Action<TAccount> AddOrEditAccountEvent;
        readonly IAccountService _accountService;

        public readonly AccountWindow _window;
        public AccountWindowViewModel(
            IAccountService accountService,
            AccountWindow accountWindow)
        {
            _accountService = accountService;
            _window = accountWindow;
            accountWindow.DataContext = this;
        }


        private string _UniqueName;


        public string UniqueName
        {
            get { return _UniqueName; }
            set => SetProperty(ref _UniqueName, value);
        }
        private string _Password;

        public string Password
        {
            get { return _Password; }
            set => SetProperty(ref _Password, value);
        }
        private long _CreatedAt;

        public long CreatedAt
        {
            get { return _CreatedAt; }
            set => SetProperty(ref _CreatedAt, value);
        }
        private long _UpdatedAt;

        public long UpdatedAt
        {
            get { return _UpdatedAt; }
            set => SetProperty(ref _UpdatedAt, value);
        }
        private string _Remark;

        public string Remark
        {
            get { return _Remark; }
            set => SetProperty(ref _Remark, value);
        }
        private bool _IsEnabled;

        public bool IsEnabled
        {
            get { return _IsEnabled; }
            set => SetProperty(ref _IsEnabled, value);
        }
        private List<object> _Roles = new List<object>();
        public List<object> Roles
        {
            get { return _Roles; }
            set => SetProperty(ref _Roles, value);
        }

        private bool _IsEdit;
        public bool IsEdit
        {
            get { return _IsEdit; }
            set
            {
                SetProperty(ref _IsEdit, value);
                if (value)
                {
                    Title = "编辑账户";
                    OperateStr = "编辑";
                }
                else
                {
                    Title = "添加账户";
                    OperateStr = "添加";
                }
            }
        }
        private string _Title;
        public string Title
        {
            get { return _Title; }
            set => SetProperty(ref _Title, value);
        }

        private string _OperateStr;
        public string OperateStr
        {
            get { return _OperateStr; }
            set => SetProperty(ref _OperateStr, value);
        }

        private string _ErrorInfo;
        public string ErrorInfo
        {
            get { return _ErrorInfo; }
            set => SetProperty(ref _ErrorInfo, value);
        }
        private List<TRole> _RolesReource;
        public List<TRole> RolesReource
        {
            get { return _RolesReource; }
            set => SetProperty(ref _RolesReource, value);
        }

        /// <summary>
        /// 添加账户使用
        /// </summary>
        public void Show()
        {
            IsEdit = false;
            _window.PasswordPanel.Visibility = Visibility.Visible;
            this.UniqueName = "";
            _window.PasswordBox1.Clear();
            _window.PasswordBox2.Clear();
            this.Remark = "";
            this.IsEnabled = true;
            this.ErrorInfo = "";
            _window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.RolesReource = _accountService.GetAllRoles();
            this.Roles = null;
            _window.Show();
            _window.Activate();
        }
        /// <summary>
        /// 编辑账户使用
        /// </summary>
        /// <param name="account"></param>
        public void Show(TAccount account)
        {
            IsEdit = true;
            _window.PasswordPanel.Visibility = Visibility.Collapsed;
            this.UniqueName = account.UniqueName;
            _window.PasswordBox1.Clear();
            _window.PasswordBox2.Clear();

            this.Remark = account.Remark;
            this.IsEnabled = account.IsEnabled;
            this.ErrorInfo = "";
            this.RolesReource = _accountService.GetAllRoles();
            List<object> roles = new List<object>();

            foreach (TRole role in account.Roles)
            {
                roles.Add(role);
            }
            this.Roles = roles;
            _window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            _window.Show();
            _window.Activate();
        }

        #region command
        public DelegateCommand OperateCmd => new DelegateCommand(Operate);

        private void Operate()
        {
            TAccount account = new TAccount();
            account.UniqueName = this.UniqueName;
            account.Password = _window.PasswordBox1.Password;
            account.Remark = this.Remark;
            account.Roles = new List<TRole>();

            foreach (TRole role in this.Roles)
                account.Roles.Add(role);
            account.IsEnabled = this.IsEnabled;
            if (IsEdit)
            {
                TCommonReply commonReply = _accountService.UpdateAccount(account);
                if (commonReply.IsSuccess)
                {
                    ErrorInfo = "更新账号成功";
                    AddOrEditAccountEvent?.Invoke(account);
                }
                else
                {
                    ErrorInfo = commonReply.Message;
                }
            }
            else
            {
                TCommonReply commonReply = _accountService.AddAccount(account);
                if (commonReply.IsSuccess)
                {
                    ErrorInfo = "添加账号成功";
                    AddOrEditAccountEvent?.Invoke(account);
                }
                else
                {
                    ErrorInfo = commonReply.Message;
                }
            }
        }
        public DelegateCommand CancelCmd => new DelegateCommand(Cancel);

        private void Cancel()
        {
            _window.Hide();
        }

        #endregion


    }
}

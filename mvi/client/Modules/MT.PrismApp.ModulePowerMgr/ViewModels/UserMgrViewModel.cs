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
    public class UserMgrViewModel : BindableBase
    {

        AccountWindowViewModel _accountWindowViewModel;
        ChangePasswordWindow _changePasswordWindow;
        readonly IAccountService _accountService;

        public UserMgrViewModel(
            IAccountService accountService)
        {
            _accountService = accountService;
            AccountWindow accountWindow = new AccountWindow();
            _accountWindowViewModel = new AccountWindowViewModel(accountService, accountWindow);
            _changePasswordWindow = new ChangePasswordWindow(accountService);
            _accountWindowViewModel.AddOrEditAccountEvent += OnAddOrEditAccountEvent;

            PageIndex = 1;
            PageSize = 10;
            GetAccounts();
        }

        void OnAddOrEditAccountEvent(TAccount account)
        {
            GetAccounts();
        }
        private int _PageSize;
        public int PageSize
        {
            get { return _PageSize; }
            set => SetProperty(ref _PageSize, value);
        }
        private int _PageCount;
        public int PageCount
        {
            get { return _PageCount; }
            set => SetProperty(ref _PageCount, value);
        }
        private int _PageIndex;
        public int PageIndex
        {
            get { return _PageIndex; }
            set => SetProperty(ref _PageIndex, value);
        }


        private readonly List<TAccount> Users;
        private List<TAccount> _CurrentPageUsers;
        public List<TAccount> CurrentPageUsers
        {
            get => _CurrentPageUsers;
            set => SetProperty(ref _CurrentPageUsers, value);
        }

        public List<TAccount> getUsers()
        {
            List<TAccount> accounts = new List<TAccount>();

            for (int i = 1; i <= 100; i++)
            {
                accounts.Add(new TAccount()
                {
                    UniqueName = "name" + i,
                    Remark = "reamrk" + i,
                    IsEnabled = i % 2 == 0,
                    Roles = new List<TRole> { new TRole() { RoleName = "admin" }, new TRole { RoleName = "user" } }
                }); ;
            }
            return accounts;
        }
        public void GetAccounts()
        {
            //CurrentPageUsers = Users.Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
            //PageCount = (Users.Count % PageSize) == 0 ? Users.Count / PageSize : Users.Count / PageSize + 1;

            //return;
            TPageAccount pageAccount = _accountService.GetAccountsByPage(PageIndex, PageSize);
            PageCount = (pageAccount.TotalCount % PageSize) == 0 ? pageAccount.TotalCount / PageSize : pageAccount.TotalCount / PageSize + 1;
            CurrentPageUsers = pageAccount.Data;
        }


        #region command
        // 页码改变命令
        public DelegateCommand<FunctionEventArgs<int>> PageUpdatedCmd => new DelegateCommand<FunctionEventArgs<int>>(PageUpdated);

        // 页码改变
        private void PageUpdated(FunctionEventArgs<int> info)
        {
            PageIndex = info.Info;
            GetAccounts();
        }


        // AddAcountCmd 
        public DelegateCommand AddAcountCmd => new DelegateCommand(AddAcount);

        private void AddAcount()
        {
            _accountWindowViewModel.Show();

        }

        // DeleteAcountCmd 
        public DelegateCommand<TAccount> DeleteAcountCmd => new DelegateCommand<TAccount>(DeleteAcount);

        private void DeleteAcount(TAccount account)
        {

            MessageBoxResult messageBoxResult = HandyControl.Controls.MessageBox.Show(
                $"确认要删除账户:{account.UniqueName} 吗?",
                "删除确认", MessageBoxButton.YesNo,
                MessageBoxImage.Question,
                MessageBoxResult.No);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                TCommonReply commonReply = _accountService.DelAccount(account.UniqueName);
                if (commonReply.IsSuccess)
                {
                  
                    HandyControl.Controls.MessageBox.Show(
                        $"删除账户{account.UniqueName}成功",
                        "删除账户成功",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information,
                        MessageBoxResult.Yes);

                    GetAccounts();
                }
                else
                {
                    HandyControl.Controls.MessageBox.Show(
                        $"删除账户{account.UniqueName}失败，失败原因：{commonReply.Message}",
                        "删除账户失败",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error,
                        MessageBoxResult.Yes);
                }
        
            }

        }

        // EditAcountCmd 
        public DelegateCommand<TAccount> EditAcountCmd => new DelegateCommand<TAccount>(EditAcount);

        private void EditAcount(TAccount account)
        {
            _accountWindowViewModel.Show(account);
        }

        // EditAcountCmd 
        public DelegateCommand<TAccount> ChangePasswordCmd => new DelegateCommand<TAccount>(ChangePassword);

        private void ChangePassword(TAccount account)
        {
            _changePasswordWindow.Show(account);
        }
        #endregion


    }
}

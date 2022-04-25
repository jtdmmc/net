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
    public class RoleMgrViewModel : BindableBase
    {
        readonly IAccountService _accountService;
        readonly RoleWindow _roleWindow;
        public RoleMgrViewModel(IAccountService accountService)
        {
            _accountService = accountService;
            _roleWindow = new RoleWindow(accountService);
            _roleWindow.AddOrEditRoleEvent += OnAddOrEditRoleEvent;
            GetRoles();
        }


        void OnAddOrEditRoleEvent()
        {
            GetRoles();
        }



        private List<TRole> _Roles;
        public List<TRole> Roles
        {
            get => _Roles;
            set => SetProperty(ref _Roles, value);
        }

        public void GetRoles()
        {
            Roles = _accountService.GetAllRoles();

        }


        #region command

        // AddAcountCmd 
        public DelegateCommand AddRoleCmd => new DelegateCommand(AddRole);

        private void AddRole()
        {
            _roleWindow.Show();
        }

        // DeleteAcountCmd 
        public DelegateCommand<TRole> DeleteRoleCmd => new DelegateCommand<TRole>(DeleteRole);

        private void DeleteRole(TRole role)
        {

            MessageBoxResult messageBoxResult = HandyControl.Controls.MessageBox.Show(
                $"确认要删除角色:{role.RoleName} 吗?",
                "删除确认", MessageBoxButton.YesNo,
                MessageBoxImage.Question,
                MessageBoxResult.No);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                TCommonReply commonReply = _accountService.DelRole(role.RoleId);
                if (commonReply.IsSuccess)
                {

                    HandyControl.Controls.MessageBox.Show(
                        $"删除角色{role.RoleName}成功",
                        "删除账户成功",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information,
                        MessageBoxResult.Yes);

                    GetRoles();
                }
                else
                {
                    HandyControl.Controls.MessageBox.Show(
                        $"删角色{role.RoleName}失败，失败原因：{commonReply.Message}",
                        "删除账户失败",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error,
                        MessageBoxResult.Yes);
                }

            }
        }

        // EditAcountCmd 
        public DelegateCommand<TRole> EditRoleCmd => new DelegateCommand<TRole>(EditRole);

        private void EditRole(TRole role)
        {
            _roleWindow.Show(role);
        }
        #endregion

    }
}

using MT.PrismApp.ModulePowerMgr.Views;
using MT.PrismApp.Services.Interfaces;
using MVI.DotnetSoftwarePlatform.Api.Thrift;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MT.PrismApp.ModulePowerMgr.ViewModels
{
    public class PowerMgrViewModel : BindableBase, INavigationAware
    {
        readonly IAccountService _accountService;
        readonly UserMgrViewModel _userMgrViewModel;
        readonly RoleMgrViewModel _roleMgrViewModel;
        public PowerMgrViewModel(IAccountService accountService)
        {
            _accountService = accountService;
            _userMgrViewModel = new UserMgrViewModel(accountService);
            _roleMgrViewModel = new RoleMgrViewModel(accountService);
        }

        public UserMgrViewModel UserMgrViewModel { get => _userMgrViewModel; }
        public RoleMgrViewModel RoleMgrViewModel { get => _roleMgrViewModel; }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {

        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return false;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

    }
}

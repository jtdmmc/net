using log4net;
using MT.PrismApp.Common.Consts;
using MT.PrismApp.Common.PrismEvents;
using MT.PrismApp.Views;
using MVI.DotnetSoftwarePlatform.Api.Thrift;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;

namespace MT.PrismApp.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        public static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        IRegionManager _regionManager;
        IEventAggregator _eventAggregator;
        LoginWindowViewModel _loginWindowViewModel;
        MainWindow _mainWindow;
        AboutWindow aboutWindow;
        public MainWindowViewModel(
            IEventAggregator eventAggregator,
            IRegionManager _RegionManager,
            MainWindow mainWindow,
            LoginWindowViewModel loginWindowViewModel)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<OpenMenuEvent>().Subscribe(OnOpenMenuEvent, ThreadOption.UIThread, false);

            _regionManager = _RegionManager;
            _mainWindow = mainWindow;
            _mainWindow.DataContext = this;
            _loginWindowViewModel = loginWindowViewModel;
            _loginWindowViewModel._mainWindowViewModel = this;
            aboutWindow = new AboutWindow();
        }

        void OnOpenMenuEvent(OpenMenuEventParm openMenuEventParm)
        {
            Log.Info($"Event Do ：{openMenuEventParm}");
            foreach (var menu in Menus)
            {
                if (menu.RegionName == openMenuEventParm.RegionName)
                {
                    menu.IsSelected = true;
                    break;
                }
            }
        }
        private string _UserName = "Admin";
        public string UserName
        {
            get { return _UserName; }
            set { SetProperty(ref _UserName, value); }
        }
        private TAccount _Account;
        public TAccount Account
        {
            get { return _Account; }
            set { SetProperty(ref _Account, value); }
        }

        private bool _LoggedIn = false;
        public bool LoggedIn
        {
            get { return _LoggedIn; }
            set
            {
                SetProperty(ref _LoggedIn, value);
            }
        }
        private List<MenuViewModel> _Menus;
        public List<MenuViewModel> Menus
        {
            get { return _Menus; }
            set { SetProperty(ref _Menus, value); }
        }
        public void Show()
        {
            if (LoggedIn)
            {

                Menus = MenuViewModel.GetMenus(Account);
                _loginWindowViewModel.Hide();
                _mainWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                _mainWindow.Activate();
                _mainWindow.Show();

                OpenMenuEventParm openMenuEventParm = new OpenMenuEventParm()
                {
                    RegionName = RegionNames.Workbench
                };
                Log.Info($"Event Trriger OpenMenuEvent {openMenuEventParm }");
                _eventAggregator.GetEvent<OpenMenuEvent>().Publish(openMenuEventParm);
            }
            else
            {

                _mainWindow.Hide();
                _loginWindowViewModel.Show();
            }
        }


        private DelegateCommand _closeCommand;
        public DelegateCommand CloseCommand => _closeCommand ?? (_closeCommand = new DelegateCommand(ExecuteCloseCommand));

        void ExecuteCloseCommand()
        {
            //_mainWindow.Close();

            Process.GetCurrentProcess().Kill();//此方法完全奏效，绝对是完全退出。
        }

        private DelegateCommand _SwitchUserCommand;
        public DelegateCommand SwitchUserCommand => _SwitchUserCommand ?? (_SwitchUserCommand = new DelegateCommand(ExecuteSwitchUserCommand));

        void ExecuteSwitchUserCommand()
        {
            this.LoggedIn = false;
            Show();
        }
        private DelegateCommand<object> _SelectedMenuCmd;
        public DelegateCommand<object> SelectedMenuCmd => _SelectedMenuCmd ?? (_SelectedMenuCmd = new DelegateCommand<object>(ExecuteSelectedMenuCmd));

        void ExecuteSelectedMenuCmd(object o)
        {
            MenuViewModel menuViewModel = (MenuViewModel)o;
            _regionManager.RequestNavigate("ContentRegion", menuViewModel.RegionName);
        }


        private DelegateCommand _ShowAboutWindowCmd;
        public DelegateCommand ShowAboutWindowCmd => _ShowAboutWindowCmd ?? (_ShowAboutWindowCmd = new DelegateCommand(ExecuteShowAboutWindowCmd));

        void ExecuteShowAboutWindowCmd()
        {
            aboutWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            aboutWindow.Activate();
            aboutWindow.Show();
        }
    }
}

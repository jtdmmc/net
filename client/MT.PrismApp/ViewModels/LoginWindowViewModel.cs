using MT.PrismApp.Services.Interfaces;
using MT.PrismApp.Views;
using MVI.DotnetSoftwarePlatform.Api.Thrift;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MT.PrismApp.ViewModels
{
    public class LoginWindowViewModel : BindableBase
    {
        LoginWindow _window;
        public MainWindowViewModel _mainWindowViewModel;
        IAccountService _loginService;
        public LoginWindowViewModel(LoginWindow window, IAccountService loginService)
        {
            _window = window;
            _window.DataContext = this;
            _loginService = loginService;

            _window.txt_Pwd.PasswordChanged += Txt_Pwd_PasswordChanged;
        }

        private void Txt_Pwd_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PassWord = _window.txt_Pwd.Password;
        }

        #region model
        private string _UserName = "Admin";
        public string UserName
        {
            get { return _UserName; }
            set { SetProperty(ref _UserName, value); }
        }
        private string _PassWord = "";
        public string PassWord
        {
            get { return _PassWord; }
            set
            {
                SetProperty(ref _PassWord, value);
                LoginCommand.RaiseCanExecuteChanged();
            }
        }

        private string _LoginInfo;
        public string LoginInfo
        {
            get { return _LoginInfo; }
            set
            {
                SetProperty(ref _LoginInfo, value);
                LoginCommand.RaiseCanExecuteChanged();

            }
        }
        #endregion


        public void Show()
        {
            _window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            _window.Activate();
            _window.Show();
        }

        public void Hide()
        {
            _window.Hide();
        }

        #region 命令
        public bool IsCanLogin
        {
            get
            {
                bool flag = !string.IsNullOrEmpty(PassWord) && !string.IsNullOrEmpty(UserName);
                return flag;
            }
        }

        private DelegateCommand _LoginCommand;
        public DelegateCommand LoginCommand =>
            _LoginCommand ?? (_LoginCommand = new DelegateCommand(ExecuteLoginCommand, CanExecuteLoginCommand));

        void ExecuteLoginCommand()
        {
            //_window.DialogResult = true;
            //return;
            TCommonReply commonReply = _loginService.SignInWithAccount(UserName, PassWord);

            if (commonReply.IsSuccess)
            {
                TAccount account = _loginService.GetAccountByUniqueName(UserName);
                _mainWindowViewModel.LoggedIn = true;
                _mainWindowViewModel.UserName = UserName;
                _mainWindowViewModel.Account = account;
                _mainWindowViewModel.Show();
            }
            else
            {
                LoginInfo = commonReply.Message;
            }
        }

        bool CanExecuteLoginCommand()
        {
            return IsCanLogin;
        }



        private DelegateCommand _closeCommand;
        public DelegateCommand CloseCommand => _closeCommand ?? (_closeCommand = new DelegateCommand(ExecuteCloseCommand));

        void ExecuteCloseCommand()
        {

            Process.GetCurrentProcess().Kill();//此方法完全奏效，绝对是完全退出。
        }

        private DelegateCommand _SwitchUserCommand;
        public DelegateCommand SwitchUserCommand => _SwitchUserCommand ?? (_SwitchUserCommand = new DelegateCommand(ExecuteSwitchUserCommand));

        void ExecuteSwitchUserCommand()
        {
            _mainWindowViewModel.LoggedIn = false;
            _mainWindowViewModel.Show();
        }
        #endregion
    }
}

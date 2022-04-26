using MT.PrismApp.Services.Interfaces;
using MVI.DotnetSoftwarePlatform.Api.Thrift;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MT.PrismApp.ModulePowerMgr.Views
{
    /// <summary>
    /// ChangePasswordWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ChangePasswordWindow : Window
    {

        readonly IAccountService _accountService;
        public ChangePasswordWindow(IAccountService accountService)
        {
            InitializeComponent();
            _accountService = accountService;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        TAccount _account;
        public void Show(TAccount account)
        {
            _account = account;
            accountLabel.Content = account.UniqueName;
            passWordt1Tb.Clear();
            passWordt2Tb.Clear();
            newPassWordtTb.Clear();
            confirmBt.IsEnabled = false;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.Show();
            this.Activate();

        }

        private void check()
        {
            string oldPassword1 = passWordt1Tb.Password;
            string oldPassword2 = passWordt2Tb.Password;
            string newPassword = newPassWordtTb.Password;
            if (!string.IsNullOrEmpty(oldPassword1) && !string.IsNullOrEmpty(oldPassword2) && oldPassword1.Equals(oldPassword2))
            {
                msgLabel1.Visibility = Visibility.Hidden;
            }
            else
            {
                msgLabel1.Visibility = Visibility.Visible;
                confirmBt.IsEnabled = false;
                return;
            }
            if (!string.IsNullOrEmpty(oldPassword1) && !string.IsNullOrEmpty(newPassword) && !oldPassword1.Equals(newPassword))
            {
                msgLabel2.Visibility = Visibility.Hidden;
                confirmBt.IsEnabled = true;
            }
            else
            {
                msgLabel2.Visibility = Visibility.Visible;
                confirmBt.IsEnabled = false;
                return;
            }
        }
        private void confirmBt_Click(object sender, RoutedEventArgs e)
        {
            string oldPassword = passWordt1Tb.Password;
            string newPassword = newPassWordtTb.Password;
            TCommonReply commonReply = _accountService.UpdatePassword(_account.UniqueName, oldPassword, newPassword);

            if (commonReply.IsSuccess)
            {
                MessageBoxResult messageBoxResult = HandyControl.Controls.MessageBox.Show(
                    $"修改账户{_account.UniqueName}密码成功",
                    "修改密码成功",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information,
                    MessageBoxResult.Yes);
            }
            else
            {
                MessageBoxResult messageBoxResult = HandyControl.Controls.MessageBox.Show(
                    $"修改账户{_account.UniqueName}密码失败，失败原因：{commonReply.Message}",
                    "修改密码成功",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information,
                    MessageBoxResult.Yes);
            }
            this.Hide();
        }

        private void cancleBt_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            check();
        }
    }
}

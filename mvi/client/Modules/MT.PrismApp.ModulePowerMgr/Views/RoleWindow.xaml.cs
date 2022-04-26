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
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class RoleWindow : Window
    {

        public Action AddOrEditRoleEvent;
        readonly IAccountService _accountService;
        public RoleWindow(IAccountService accountService)
        {
            InitializeComponent();
            _accountService = accountService;
            Powers_CheckComBox.SelectedItemsChanged += OnSelectedItemsChanged;

        }
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        private void RoleName_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string name = RoleName_TextBox.Text.ToString();
            if (!string.IsNullOrEmpty(name) && Powers_CheckComBox.SelectedItems != null)
            {
                confirm_Button.IsEnabled = true;
            }
            else
            {
                confirm_Button.IsEnabled = false;
            }
        }
        public void OnSelectedItemsChanged()
        {
            string name = RoleName_TextBox.Text.ToString();
            if (!string.IsNullOrEmpty(name) 
                && Powers_CheckComBox.SelectedItems != null 
                && Powers_CheckComBox.SelectedItems.Count > 0)
            {
                confirm_Button.IsEnabled = true;
            }
            else
            {
                confirm_Button.IsEnabled = false;
            }

        }
        bool isEdit = false;
        public new void Show()
        {
            isEdit = false;
            title_Label.Content = "添加角色";
            RoleName_TextBox.Text = "";
            RoleName_TextBox.IsEnabled = true;
            Powers_CheckComBox.ItemsSource = _accountService.GetAllPowers();
            Powers_CheckComBox.SelectedItems = null;
            Remark_TextBox.Text = "";
            Time_Panel.Visibility = Visibility.Collapsed;
            CreatedAt_Label.Content = "";
            UpdatedAt_Label.Content = "";
            confirm_Button.IsEnabled = false;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            base.Show();
            base.Activate();
        }
        public void Show(TRole role)
        {
            isEdit = true;
            title_Label.Content = "添加角色";
            RoleName_TextBox.Text = role.RoleName;
            RoleName_TextBox.IsEnabled = false;
            Powers_CheckComBox.ItemsSource = _accountService.GetAllPowers();
            Powers_CheckComBox.SelectedItems = role.Powers;
            Remark_TextBox.Text = "";
            Time_Panel.Visibility = Visibility.Visible;
            CreatedAt_Label.Content = "";
            UpdatedAt_Label.Content = "";
            confirm_Button.IsEnabled = true;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            base.Show();
            base.Activate();
        }

        private void confirm_Button_Click(object sender, RoutedEventArgs e)
        {
            TRole role = new TRole();
            role.RoleName = RoleName_TextBox.Text;
            if (Powers_CheckComBox.SelectedItems != null)
            {
                List<TPower> powers = new List<TPower>();
                foreach (TPower power in Powers_CheckComBox.SelectedItems)
                {
                    powers.Add(power);
                }
                role.Powers = powers;
            }
            if (isEdit)
            {
                TCommonReply commonReply = _accountService.UpdateRole(role);
                if (commonReply.IsSuccess)
                {
                    MessageBoxResult messageBoxResult = HandyControl.Controls.MessageBox.Show(
                        $"修改角色{role.RoleName}成功",
                        "修改角色成功",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information,
                        MessageBoxResult.Yes);
                    AddOrEditRoleEvent?.Invoke();
                }
                else
                {
                    MessageBoxResult messageBoxResult = HandyControl.Controls.MessageBox.Show(
                        $"修改角色{role.RoleName}失败，失败原因：{commonReply.Message}",
                        "修改角色失败",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information,
                        MessageBoxResult.Yes);
                }
            }
            else
            {
                TCommonReply commonReply = _accountService.AddRole(role);
                if (commonReply.IsSuccess)
                {
                    MessageBoxResult messageBoxResult = HandyControl.Controls.MessageBox.Show(
                        $"添加角色{role.RoleName}成功",
                        "添加角色成功",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information,
                        MessageBoxResult.Yes);
                    AddOrEditRoleEvent?.Invoke();
                }
                else
                {
                    MessageBoxResult messageBoxResult = HandyControl.Controls.MessageBox.Show(
                        $"添加角色{role.RoleName}失败，失败原因：{commonReply.Message}",
                        "添加角色失败",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information,
                        MessageBoxResult.Yes);
                }
            }
        }

        private void cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

    }
}
